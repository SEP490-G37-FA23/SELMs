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





		[Fact]
		public void TestSaveManyEquipment_ReturnNothing()
		{
			try
			{
				List<Equipment> equipments = new()
				{
					new Equipment() { equipment_name = "New one" },
					new Equipment() { equipment_name = "New one 1" },
					new Equipment() { equipment_name = "New one 2" }
				};

				equipmentRepository.SaveEquipments(equipments);
				output.WriteLine($"Created multiple equipment successfully");
			}
			catch (Exception e)
			{
				Assert.Fail(e.ToString());
			}
		}





		[Theory]
		[MemberData(nameof(EquipmentRepositoryTestData.UpdateEquipmentTestData), MemberType = typeof(EquipmentRepositoryTestData))]
		public void TestUpdateEquipment_ReturnNothing(Equipment equipment)
		{
			try
			{
				equipmentRepository.UpdateEquipment(equipment);
				output.WriteLine($"Update equipment successfully");
			}
			catch (Exception e)
			{
				Assert.Fail(e.ToString());
			}
		}




		[Fact]
		public void TestGetLastEquipment_ReturnLastEquipmentInDB()
		{
			try
			{
				Equipment equipment = equipmentRepository.GetLastEquipment();
				Assert.Equal(11, equipment.equipment_id);
			}
			catch (Exception e)
			{
				Assert.Fail(e.ToString());
			}
		}



		[Theory]
		[InlineData("E0000")]
		[InlineData("E0009")]
		public void TestGetListComponentEquips_ReturnListComponentEquips(string code)
		{
			try
			{
				var list = equipmentRepository.GetListComponentEquips(code);
				Assert.NotNull(list);

				if (list.Count > 0)
				{
					foreach (var item in list)
						output.WriteLine(JsonConvert.SerializeObject(item));
				}
				else
					output.WriteLine("Components not found");
			}
			catch (Exception e)
			{
				Assert.Fail(e.ToString());
			}
		}



		[Theory]
		[MemberData(nameof(EquipmentRepositoryTestData.SaveEquipmentWithComponentTestData), MemberType = typeof(EquipmentRepositoryTestData))]
		public void TestSaveEquipmentWithComponent_ReturnNothing(Equipment equipment, int location_id, List<EquipComponentDTO> ListComponentEquips)
		{
			try
			{
				equipmentRepository.SaveEquipment(equipment, location_id, ListComponentEquips);
				output.WriteLine("Test case passed");
			}
			catch (Exception e)
			{
				Assert.Fail(e.ToString());
			}
		}
		#endregion
	}















	public static class EquipmentRepositoryTestData
	{

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
			yield return new object[] { new Equipment() { equipment_id = 11, equipment_name = "halo" } };
			yield return new object[] { new Equipment() { equipment_id = 9, equipment_name = "new update" } };
		}


	}
}
