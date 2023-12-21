using System.Collections;
using System.Web.Http.Results;

namespace SELMs.Test.Controllers.Test
{
	public class EquipmentLocationHistoryControllerTest
	{
		private readonly ITestOutputHelper output;
		private readonly ApiEquipmentLocationHistoryController apiEquipmentLocationHistoryController = new();

		public EquipmentLocationHistoryControllerTest(ITestOutputHelper output)
		{
			this.output = output;
			ApiControllerSetup.SetupController(apiEquipmentLocationHistoryController);
		}


		#region Iteration 3


		[Fact]
		public async Task TestGetEquipmentLocationHistoryList_ReturnList()
		{
			var actionResult = await apiEquipmentLocationHistoryController.GetListEquipmentLocationHistory();
			var response = await actionResult.ExecuteAsync(CancellationToken.None);
			string content = await response.Content.ReadAsStringAsync();

			IList list = JsonConvert.DeserializeObject<IList>(content);

			foreach (var item in list)
				output.WriteLine(JsonConvert.SerializeObject(item));
		}





		[Theory]
		[MemberData(nameof(EquipmentLocationHistoryControllerTestData.CreateHistoryTestData), MemberType = typeof(EquipmentLocationHistoryControllerTestData))]
		public async Task TestCreateEquipmentLocationHistory_ReturnSuccessOrFailMessage(bool expectedException, EquipmentLocationHistoryDTO equipmentLocationHistoryDTO)
		{
			var actionResult = await apiEquipmentLocationHistoryController.AddEquipmentLocationHistory(equipmentLocationHistoryDTO);
			var response = await actionResult.ExecuteAsync(CancellationToken.None);
			string content = await response.Content.ReadAsStringAsync();


			if (expectedException)
			{
				Assert.Equal(400, (int)response.StatusCode);
				Assert.IsType<BadRequestErrorMessageResult>(actionResult);
				output.WriteLine(content);
			}
			else
			{
				Assert.Equal(200, (int)response.StatusCode);
				output.WriteLine(content);
			}
		}





		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		public async Task TestGetEquipmentLocationHistoryById_ReturnHistory(int id)
		{

			var actionResult = await apiEquipmentLocationHistoryController.GetEquipmentLocationHistoryById(id);
			var response = await actionResult.ExecuteAsync(CancellationToken.None);
			string content = await response.Content.ReadAsStringAsync();

			if (content.Equals("null"))
				output.WriteLine("History not found");
			else
				output.WriteLine(content);

		}






		[Theory]
		[MemberData(nameof(EquipmentLocationHistoryControllerTestData.UpdateHistoryTestData), MemberType = typeof(EquipmentLocationHistoryControllerTestData))]
		public async Task TestUpdateEquipmentLocationHistory_ReturnSuccessOrFailMessage(bool expectedException, int id, EquipmentLocationHistoryDTO equipmentLocationHistoryDTO)
		{
			var actionResult = await apiEquipmentLocationHistoryController.UpdateEquipmentLocationHistory(id, equipmentLocationHistoryDTO);
			var response = await actionResult.ExecuteAsync(CancellationToken.None);
			string content = await response.Content.ReadAsStringAsync();



			if (expectedException)
			{
				Assert.Equal(400, (int)response.StatusCode);
				Assert.IsType<BadRequestErrorMessageResult>(actionResult);
				output.WriteLine(content);
			}
			else
			{
				output.WriteLine(content);
			}

		}
		#endregion
	}



	internal static class EquipmentLocationHistoryControllerTestData
	{
		public static IEnumerable<object[]> CreateHistoryTestData()
		{
			yield return new object[] { true, null! };
			yield return new object[] { false, new EquipmentLocationHistoryDTO() { location_id = 1, system_equipment_code = "E0010" } };
			yield return new object[] { false, new EquipmentLocationHistoryDTO() { location_id = 2, system_equipment_code = "" } };
		}


		public static IEnumerable<object[]> UpdateHistoryTestData()
		{
			yield return new object[] { true, 0, null! };
			yield return new object[] { false, 0, new EquipmentLocationHistoryDTO() { location_id = 0, system_equipment_code = "E0010" } };
			yield return new object[] { false, 1, new EquipmentLocationHistoryDTO() { location_id = 1, system_equipment_code = "E0021" } };
		}
	}
}
