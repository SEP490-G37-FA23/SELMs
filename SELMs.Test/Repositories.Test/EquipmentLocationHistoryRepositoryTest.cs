namespace SELMs.Test.Repositories.Test
{
	public class EquipmentLocationHistoryRepositoryTest
	{
		private readonly ITestOutputHelper output;
		private readonly IEquipmentLocationHistoryRepository equipmentLocationHistoryRepository = new EquipmentLocationHistoryRepository();


		public EquipmentLocationHistoryRepositoryTest(ITestOutputHelper output)
		{
			this.output = output;
		}



		[Fact]
		public async Task TestGetEquipmentLocationHistoryList_ReturnList()
		{
			var list = await equipmentLocationHistoryRepository.GetEquipmentLocationHistoryList();

			foreach (var item in list)
				output.WriteLine(JsonConvert.SerializeObject(item));
		}



		[Theory]
		[InlineData(0)]
		[InlineData(14)]
		public async Task TestGetEquipmentLocationHistoryById_ReturnHistory(int id)
		{
			Equipment_Location_History history = await equipmentLocationHistoryRepository.GetEquipmentLocationHistoryById(id);

			if (history != null)
				output.WriteLine(JsonConvert.SerializeObject(history));
			else
				output.WriteLine("History not found");
		}





		[Theory]
		[MemberData(nameof(EquipmentLocationHistoryRepositoryTestData.CreateHistoryTestData), MemberType = typeof(EquipmentLocationHistoryRepositoryTestData))]
		public async Task TestCreateEquipmentLocationHistory_ReturnCreatedStatus(Equipment_Location_History history)
		{
			try
			{
				bool createdSuccesfull = await equipmentLocationHistoryRepository.Add(history);

				if (createdSuccesfull)
					output.WriteLine("Create successfull");
				else
					output.WriteLine("Create fail");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}


		[Theory]
		[MemberData(nameof(EquipmentLocationHistoryRepositoryTestData.UpdateHistoryTestData), MemberType = typeof(EquipmentLocationHistoryRepositoryTestData))]
		public async Task TestUpdateEquipmentLocationHistory_ReturnCreatedStatus(Equipment_Location_History history)
		{
			try
			{
				bool updateSuccesfull = await equipmentLocationHistoryRepository.Update(history);

				if (updateSuccesfull)
					output.WriteLine("Update successfull");
				else
					output.WriteLine("Update fail");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}
	}







	internal static class EquipmentLocationHistoryRepositoryTestData
	{
		public static IEnumerable<object[]> CreateHistoryTestData()
		{
			yield return new object[] { new Equipment_Location_History() { location_id = 1, system_equipment_code = "E1111" } };
			yield return new object[] { new Equipment_Location_History() { location_id = 5, system_equipment_code = "E2222" } };
		}


		public static IEnumerable<object[]> UpdateHistoryTestData()
		{
			yield return new object[] { new Equipment_Location_History() { id = 0, location_id = 0, system_equipment_code = "E0010" } };
			yield return new object[] { new Equipment_Location_History() { id = 33, location_id = 8, system_equipment_code = "E0021" } };
		}
	}
}
