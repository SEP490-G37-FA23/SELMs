namespace SELMs.Test.Services.Test
{
	public class EquipmentServiceTest
	{

		private readonly ITestOutputHelper output;
		private readonly IEquipmentService equipmentService = new EquipmentService();

		public EquipmentServiceTest(ITestOutputHelper output)
		{
			this.output = output;
		}







		#region Iteration 2

		[Theory]
		[MemberData(nameof(EquipmentServiceTestData.CreateEquipmentTestData), MemberType = typeof(EquipmentServiceTestData))]
		public async Task TestSaveEquipment_ReturnNothing(Equipment equipment, int location_id, List<EquipComponentDTO> ListComponentEquips)
		{
			try
			{
				await equipmentService.SaveEquipment(equipment, location_id, ListComponentEquips);
				output.WriteLine("Test case passed");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}





		[Theory]
		[MemberData(nameof(EquipmentServiceTestData.ImportEquipmentTestData), MemberType = typeof(EquipmentServiceTestData))]
		public async Task TestImportEquipment_ReturnNothing(List<Equipment> equipment)
		{
			try
			{
				await equipmentService.ImportEquipments(equipment);
				output.WriteLine("Test case passed");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}


		[Theory]
		[MemberData(nameof(EquipmentServiceTestData.UpdateEquipmentTestData), MemberType = typeof(EquipmentServiceTestData))]
		public async Task TestUpdateEquipment_ReturnNothing(int id, Equipment equipment)
		{
			try
			{
				await equipmentService.UpdateEquipment(id, equipment);
				output.WriteLine("Test case passed");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}
		#endregion
	}







	public static class EquipmentServiceTestData
	{


		public static IEnumerable<object[]> CreateEquipmentTestData()
		{

			Equipment e1 = new() { system_equipment_code = "E0987", equipment_name = "quein sera" };
			Equipment e2 = new() { system_equipment_code = "E0654", equipment_name = "F4 Phantom" };


			List<EquipComponentDTO> list1 = new()
			{
				new EquipComponentDTO() {system_equipment_code="E098",equipment_name = "Eq" },
				new EquipComponentDTO() {system_equipment_code="E765",equipment_name = "Eq1" }
			};

			List<EquipComponentDTO> list2 = new()
			{
				new EquipComponentDTO() {system_equipment_code="432",equipment_name = "Eq2" },
				new EquipComponentDTO() {system_equipment_code="211",equipment_name = "Eq3" }
			};

			yield return new object[] { e1, 1, list1 };
			yield return new object[] { e2, 1, list2 };
		}






		public static IEnumerable<object[]> ImportEquipmentTestData()
		{
			List<Equipment> list1 = new()
			{
				new Equipment(){equipment_name="MP5"},
				new Equipment(){equipment_name="AK-47"}
			};

			List<Equipment> list2 = new()
			{
				new Equipment(){ equipment_name="Mosin Nagant"},
				new Equipment(){ equipment_name="Karabiner 98k"}
			};

			yield return new object[] { list1 };
			yield return new object[] { list2 };
		}






		public static IEnumerable<object[]> UpdateEquipmentTestData()
		{
			yield return new object[] { 0, new Equipment() { system_equipment_code = "E0010", equipment_name = "no non" } };
			yield return new object[] { 20, new Equipment() { system_equipment_code = "E0020", equipment_name = "ok bro" } };
		}

	}
}
