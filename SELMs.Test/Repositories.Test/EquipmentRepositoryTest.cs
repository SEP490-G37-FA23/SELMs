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
		[MemberData(nameof(EquipmentRepositoryTestData.GetEquipmentListTestData), MemberType = typeof(EquipmentRepositoryTestData))]
		public async Task TestGetEquipmentListByMultiCondition_ReturnEquipmentList(Argument argument)
		{
			var list = await equipmentRepository.GetEquipmentList(argument);

			foreach (var item in list)
				output.WriteLine(JsonConvert.SerializeObject(item));
		}



		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(5)]
		[InlineData(int.MaxValue)]
		public void TestGetEquipmentById_ReturnEquipmentFound(int id)
		{
			Equipment equipment = equipmentRepository.GetEquipment(id);

			if (equipment is null)
				output.WriteLine("Equipment not found");

			else
				output.WriteLine(JsonConvert.SerializeObject(equipment));
		}





		[Theory]
		[InlineData("")]
		[InlineData("E00000000")]
		[InlineData("E0006")]
		public async Task TestGetEquipmentByCode_ReturnEquipmentFound(string code)
		{
			var equipment = await equipmentRepository.GetDetailEquipment(code);

			if ((equipment as List<dynamic>)!.Count <= 0)
				output.WriteLine("Equipment not found");

			else
				output.WriteLine(JsonConvert.SerializeObject(equipment));
		}





		[Fact]
		public void TestSaveManyEquipment_ReturnNothing()
		{
			SELMsContext sELMsContext = new();

			List<Equipment> equipments = new()
				{
					new Equipment() { equipment_name = "New one" },
					new Equipment() { equipment_name = "" },
					new Equipment() { equipment_name = "N" }
				};
			equipmentRepository.SaveEquipments(equipments);
			Thread.Sleep(1000);

			var e1 = sELMsContext.Equipments.FirstOrDefault(e => e.equipment_name.Equals("New one"));
			var e2 = sELMsContext.Equipments.FirstOrDefault(e => e.equipment_name.Equals(""));
			var e3 = sELMsContext.Equipments.FirstOrDefault(e => e.equipment_name.Equals("N"));

			Assert.Equal("New one", e1.equipment_name);
			Assert.Equal("", e2.equipment_name);
			Assert.Equal("N", e3.equipment_name);

			output.WriteLine($"Created many equipments successfully");
		}





		[Theory]
		[MemberData(nameof(EquipmentRepositoryTestData.UpdateEquipmentTestData), MemberType = typeof(EquipmentRepositoryTestData))]
		public void TestUpdateEquipment_ReturnNothing(Equipment equipment)
		{
			try
			{
				SELMsContext sELMsContext = new();
				equipmentRepository.UpdateEquipment(equipment);
				Thread.Sleep(1500);

				var e = sELMsContext.Equipments.FirstOrDefault(e => e.equipment_id == equipment.equipment_id);

				Assert.Equal(equipment.equipment_name, e.equipment_name);
				output.WriteLine("Updated successfully");
			}
			catch (Exception e)
			{
				Assert.IsType<ArgumentNullException>(e);
				output.WriteLine(e.Message);
			}
		}




		[Fact]
		public void TestGetLastEquipment_ReturnLastEquipmentInDB()
		{
			Equipment equipment = equipmentRepository.GetLastEquipment();
			output.WriteLine(JsonConvert.SerializeObject(equipment));
		}



		[Theory]
		[InlineData("E0000")]
		[InlineData("E0008")]
		public void TestGetListComponentEquips_ReturnListComponentEquips(string code)
		{
			var list = equipmentRepository.GetListComponentEquips(code);

			if (list.Count > 0)
				foreach (var item in list)
					output.WriteLine(JsonConvert.SerializeObject(item));
			else
				output.WriteLine("Components not found");
		}






		[Theory]
		[InlineData(0)]
		[InlineData(86)]
		public void TestDeleteEquipment_ReturnNoException(int id)
		{
			SELMsContext sELMsContext = new();
			var equipment = sELMsContext.Equipments.FirstOrDefault(e => e.equipment_id == id);

			if (equipment != null)
			{
				equipmentRepository.DeleteEquipment(id);
				Thread.Sleep(2000);
				output.WriteLine("Delete successfull");
			}
			else
				output.WriteLine("Equipment not found to delete");
		}






		[Theory]
		[MemberData(nameof(EquipmentRepositoryTestData.SaveEquipmentWithComponentTestData), MemberType = typeof(EquipmentRepositoryTestData))]
		public void TestSaveEquipmentWithComponent_ReturnNothing(Equipment equipment, int location_id, List<EquipComponentDTO> ListComponentEquips)
		{

			equipmentRepository.SaveEquipment(equipment, location_id, ListComponentEquips);
			output.WriteLine("Equipment created successfully");

		}
		#endregion
	}















	public static class EquipmentRepositoryTestData
	{


		public static IEnumerable<object[]> GetEquipmentListTestData()
		{
			//text = Equipment.serial_no , text1 = user fullname, text2 = std_code, sys_code, name
			var a = new Argument() { text = "SELT32502623-8225", text1 = "Mai Thị Ly", text2 = "E0003" };
			var b = new Argument() { text = "SELT32502623-8225", text1 = "Mai Thị Ly", text2 = "BHR4975EU" };
			var c = new Argument() { text = "SELT32502623-8225", text1 = "Mai Thị Ly", text2 = "Màn hình máy tính Xiaomi 27\" 1C BHR4975EU" };


			var d = new Argument() { text = "FKH8857823-349056", text1 = "Lê Tất Đạt", text2 = string.Empty };
			var e = new Argument() { text = "FKH8857823-349056", text1 = "Lê Tất Đạt", text2 = "Xiaomi" };
			var f = new Argument() { text = "FKH8857823-349056", text1 = "Lê Tất Đạt", text2 = "levan" };

			yield return new object[] { a };
			yield return new object[] { b };
			yield return new object[] { c };
			yield return new object[] { d };
			yield return new object[] { e };
			yield return new object[] { f };

		}




		public static IEnumerable<object[]> SaveEquipmentWithComponentTestData()
		{
			Equipment e1 = new() { system_equipment_code = "E0009", equipment_name = "test 1" };
			Equipment e2 = new() { system_equipment_code = "E0010", equipment_name = "test 2" };


			List<EquipComponentDTO> list1 = new()
			{
				new EquipComponentDTO(){system_equipment_code="E0001"},
				new EquipComponentDTO(){system_equipment_code="E0002"}
			};

			List<EquipComponentDTO> list2 = new()
			{
				new EquipComponentDTO(){system_equipment_code="E0003"},
				new EquipComponentDTO(){system_equipment_code="E0004"}
			};

			yield return new object[] { e1, 0, list1 };
			yield return new object[] { e2, 1, list2 };
		}



		public static IEnumerable<object[]> UpdateEquipmentTestData()
		{
			yield return new object[] { new Equipment() { equipment_id = 0, equipment_name = "halo" } };
			yield return new object[] { new Equipment() { equipment_id = 9, equipment_name = "G" } };
		}


	}
}
