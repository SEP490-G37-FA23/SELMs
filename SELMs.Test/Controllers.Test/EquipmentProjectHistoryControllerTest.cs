namespace SELMs.Test.Controllers.Test
{
	public class EquipmentProjectHistoryControllerTest
	{

		private readonly ITestOutputHelper output;
		private readonly ApiEquipmentProjectHistoryController apiEquipmentProjectHistoryController = new();


		public EquipmentProjectHistoryControllerTest(ITestOutputHelper output)
		{
			this.output = output;
			ApiControllerSetup.SetupController(apiEquipmentProjectHistoryController);
		}



		[Fact]
		public async Task TestGetListHistory_ReturnHistoryList()
		{
			var actionResult = await apiEquipmentProjectHistoryController.GetListEquipmentProjectHistory();
			var response = await actionResult.ExecuteAsync(CancellationToken.None);
			string content = await response.Content.ReadAsStringAsync();


			var list = JsonConvert.DeserializeObject<List<Equipment_Project_History>>(content);

			Assert.Equal(200, (int)response.StatusCode);

			if (list.Count > 0)
				foreach (var item in list)
					output.WriteLine(JsonConvert.SerializeObject(item));

			else
				output.WriteLine("No history found");
		}






		[Theory]
		[MemberData(nameof(EquipmentProjectHistoryControllerTestData.SaveHistoryTestData), MemberType = typeof(EquipmentProjectHistoryControllerTestData))]
		public async Task TestCreateHistory_ReturnStatusMessage(EquipmentProjectHistoryDTO equipmentProjectHistoryDTO)
		{
			try
			{
				var actionResult = await apiEquipmentProjectHistoryController.AddEquipmentProjectHistory(equipmentProjectHistoryDTO);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				string content = await response.Content.ReadAsStringAsync();

				output.WriteLine($"Test case passed - Status code: {(int)response.StatusCode}\n{content}");
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
		public async Task TestGetHistoryById_ReturnHistory(int id)
		{
			var actionResult = await apiEquipmentProjectHistoryController.GetEquipmentProjectHistoryById(id);
			var response = await actionResult.ExecuteAsync(CancellationToken.None);
			string content = await response.Content.ReadAsStringAsync();

			if (content.Equals("null"))
				output.WriteLine("Equipment not found");
			else
				output.WriteLine(content);
		}







		[Theory]
		[MemberData(nameof(EquipmentProjectHistoryControllerTestData.UpdateHistoryTestData), MemberType = typeof(EquipmentProjectHistoryControllerTestData))]
		public async Task TestUpdateHistory_ReturnStatusMessage(int id, EquipmentProjectHistoryDTO equipmentProjectHistoryDTO)
		{
			try
			{
				var actionResult = await apiEquipmentProjectHistoryController.UpdateEquipmentProjectHistory(id, equipmentProjectHistoryDTO);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				string content = await response.Content.ReadAsStringAsync();

				output.WriteLine($"Test case passed - Status code: {(int)response.StatusCode}\n{content}");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}
	}







	internal static class EquipmentProjectHistoryControllerTestData
	{
		public static IEnumerable<object[]> SaveHistoryTestData()
		{
			yield return new object[] { new EquipmentProjectHistoryDTO() { project_id = 8, system_equiment_code = "E0006" } };
			yield return new object[] { new EquipmentProjectHistoryDTO() { project_id = 10, system_equiment_code = "E0024" } };
			yield return new object[] { new EquipmentProjectHistoryDTO() { project_id = 5, system_equiment_code = "E0024" } };
		}



		public static IEnumerable<object[]> UpdateHistoryTestData()
		{
			yield return new object[] { 0, new EquipmentProjectHistoryDTO() { project_id = 8, system_equiment_code = "E0032" } };
			yield return new object[] { 1, new EquipmentProjectHistoryDTO() { project_id = 10, system_equiment_code = "E0028" } };
			yield return new object[] { 3, new EquipmentProjectHistoryDTO() { project_id = 5, system_equiment_code = "E0034" } };
		}
	}
}
