using System.Collections;

namespace SELMs.Test.Repositories.Test
{
	public class EquipmentProjectHistoryRepositoryTest
	{
		private readonly ITestOutputHelper output;
		private readonly IEquipmentProjectHistoryRepository equipmentProjectHistoryRepository = new EquipmentProjectHistoryRepository();

		public EquipmentProjectHistoryRepositoryTest(ITestOutputHelper output)
		{
			this.output = output;
		}



		[Fact]
		public async Task TestGetEquipmentList_ReturnEquipmentList()
		{
			var list = await equipmentProjectHistoryRepository.GetEquipmentProjectHistoryList();

			Assert.True(list is IList { Count: > 0 });

			foreach (var item in list)
				output.WriteLine(JsonConvert.SerializeObject(item));
		}



		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(6)]
		public async Task TestGetEquipmentById_ReturnEquipment(int id)
		{
			var e = await equipmentProjectHistoryRepository.GetEquipmentProjectHistoryById(id);

			if (e != null)
				output.WriteLine(JsonConvert.SerializeObject(e));
			else
				output.WriteLine("Equipment not found");




		}



		[Theory]
		[MemberData(nameof(EquipmentProjectHistoryRepositoryTestData.SaveHistoryTestData), MemberType = typeof(EquipmentProjectHistoryRepositoryTestData))]
		public async Task TestCreateEquipment_ReturnTrueFalse(Equipment_Project_History equipmentProjectHistory)
		{
			bool createdSuccessfull = await equipmentProjectHistoryRepository.SaveHistory(equipmentProjectHistory);

			if (createdSuccessfull)
				output.WriteLine("Create successfully");
			else
				output.WriteLine("Create fail");
		}




		[Theory]
		[MemberData(nameof(EquipmentProjectHistoryRepositoryTestData.UpdateHistoryTestData), MemberType = typeof(EquipmentProjectHistoryRepositoryTestData))]
		public async Task TestUpdateEquipment_ReturnTrueFalse(Equipment_Project_History equipmentProjectHistory)
		{
			bool updatedSuccessfull = await equipmentProjectHistoryRepository.Update(equipmentProjectHistory);

			if (updatedSuccessfull)
				output.WriteLine("Updated successfully");
			else
				output.WriteLine("Update fail");
		}
	}











	internal static class EquipmentProjectHistoryRepositoryTestData
	{
		public static IEnumerable<object[]> SaveHistoryTestData()
		{
			yield return new object[] { new Equipment_Project_History() { project_id = 22, system_equiment_code = "E0012" } };
			yield return new object[] { new Equipment_Project_History() { project_id = 15, system_equiment_code = "E0022" } };
		}



		public static IEnumerable<object[]> UpdateHistoryTestData()
		{
			yield return new object[] { new Equipment_Project_History() { id = 19, project_id = 20, system_equiment_code = "E0028" } };
			yield return new object[] { new Equipment_Project_History() { id = 15, project_id = 21, system_equiment_code = "E0034" } };
		}
	}
}
