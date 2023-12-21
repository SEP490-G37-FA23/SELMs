namespace SELMs.Test.Services.Test
{
	public class EquipmentLocationHistoryServiceTest
	{
		private readonly ITestOutputHelper output;
		private readonly IEquipmentLocationHistoryService equipmentLocationHistoryService = new EquipmentLocationHistoryService();


		public EquipmentLocationHistoryServiceTest(ITestOutputHelper output)
		{
			this.output = output;
		}


		[Theory]
		[MemberData(nameof(EquipmentLocationHistoryServiceTestData.UpdateHistoryTestData), MemberType = typeof(EquipmentLocationHistoryServiceTestData))]
		public async Task TestUpdateHistory_ReturnNoException(int id, Equipment_Location_History equipmentLocationHistory)
		{
			try
			{
				bool updateSuccess = await equipmentLocationHistoryService.UpdateEquipmentLocationHistory(id, equipmentLocationHistory);

				if (updateSuccess)
					output.WriteLine("Test case passed");
				else
					output.WriteLine("Test case fail");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}
	}


	internal static class EquipmentLocationHistoryServiceTestData
	{
		public static IEnumerable<object[]> UpdateHistoryTestData()
		{
			yield return new object[] { 0, new Equipment_Location_History() { location_id = 0, system_equipment_code = "E0010" } };
			yield return new object[] { 29, new Equipment_Location_History() { location_id = 8, system_equipment_code = "E0010", note = "Hello there" } };
		}
	}
}
