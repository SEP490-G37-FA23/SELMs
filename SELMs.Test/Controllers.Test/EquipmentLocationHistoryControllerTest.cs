using System.Collections;

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
		public async Task TestCreateEquipmentLocationHistory_ReturnSuccessOrFailMessage(EquipmentLocationHistoryDTO equipmentLocationHistoryDTO)
		{
			try
			{
				var actionResult = await apiEquipmentLocationHistoryController.AddEquipmentLocationHistory(equipmentLocationHistoryDTO);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				string content = await response.Content.ReadAsStringAsync();

				if (content.Equals("null"))
					output.WriteLine("History not found");
				else
					output.WriteLine(content);
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
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
		public async Task TestUpdateEquipmentLocationHistory_ReturnSuccessOrFailMessage(int id, EquipmentLocationHistoryDTO equipmentLocationHistoryDTO)
		{
			try
			{
				var actionResult = await apiEquipmentLocationHistoryController.UpdateEquipmentLocationHistory(id, equipmentLocationHistoryDTO);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				string content = await response.Content.ReadAsStringAsync();

				output.WriteLine(content);
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}
		#endregion
	}



	internal static class EquipmentLocationHistoryControllerTestData
	{
		public static IEnumerable<object[]> CreateHistoryTestData()
		{
			yield return new object[] { new EquipmentLocationHistoryDTO() { location_id = 1, system_equipment_code = "E0010" } };
			yield return new object[] { new EquipmentLocationHistoryDTO() { location_id = 5, system_equipment_code = "E0020" } };
		}


		public static IEnumerable<object[]> UpdateHistoryTestData()
		{
			yield return new object[] { 0, new EquipmentLocationHistoryDTO() { location_id = 0, system_equipment_code = "E0010" } };
			yield return new object[] { 30, new EquipmentLocationHistoryDTO() { location_id = 8, system_equipment_code = "E0021" } };
		}
	}
}
