using Newtonsoft.Json;
using SELMs.Repositories;
using SELMs.Repositories.Implements;

namespace SELMs.Test.Repositories.Test
{
	public class EquipmentRepositoryTest
	{

		private readonly ITestOutputHelper output;
		private readonly IEquipmentRepository equipmentRepository = new EquipmentRepository();

		public EquipmentRepositoryTest(ITestOutputHelper output)
		{
			this.output = output;
		}


		#region Iteration 2


		[Fact]
		public async Task TestGetEquipmentList_ReturnEquipmentList()
		{
			var list = await equipmentRepository.GetEquipmentList();

			Assert.True(list is List<Equipment> { Count: > 0 });

			foreach (var item in list)
				output.WriteLine(JsonConvert.SerializeObject(item));
		}







		[Theory]
		[InlineData(0)]
		[InlineData(5)]
		public void TestGetEquipmentById_ReturnEquipmentFound(int id)
		{
			Equipment equipment = equipmentRepository.GetEquipment(id);

			if (equipment is null)
				output.WriteLine("Equipment not found");

			else
				output.WriteLine($"Equipment found\n{JsonConvert.SerializeObject(equipment)}");
		}



		[Theory]
		[InlineData("E00000000")]
		[InlineData("E0006")]
		public async Task TestGetEquipmentByCode_ReturnEquipmentFound(string code)
		{
			var equipment = await equipmentRepository.GetDetailEquipment(code);

			if ((equipment as List<dynamic>)!.Count <= 0)
				output.WriteLine("Equipment not found");

			else
				output.WriteLine($"Equipment found\n{JsonConvert.SerializeObject(equipment)}");
		}
		#endregion
	}
}
