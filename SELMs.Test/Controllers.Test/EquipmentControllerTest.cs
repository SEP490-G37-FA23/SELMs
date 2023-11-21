using System.Web.Http.Results;

namespace SELMs.Test.Controllers.Test
{
	public class EquipmentControllerTest
	{

		private readonly ITestOutputHelper output;
		private readonly ApiEquipmentController apiEquipmentController = new();



		public EquipmentControllerTest(ITestOutputHelper output)
		{
			this.output = output;
			ApiControllerSetup.SetupController(apiEquipmentController);
		}








		#region Iteration 2


		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		public async Task TestGetEquipmentById_ReturnEquipmentFound(int id)
		{
			try
			{
				var actionResult = await apiEquipmentController.GetEquipment(id);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				string content = await response.Content.ReadAsStringAsync();

				if (actionResult is OkNegotiatedContentResult<Equipment>)
					output.WriteLine($"Status code: {(int)response.StatusCode}\nEquipment found:\n{content}");
				else
					output.WriteLine($"Equipment not found");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}







		[Theory]
		[InlineData("E0000")]
		[InlineData("E0002")]
		[InlineData("E0004")]
		public void TestGetEquipmentBySystemCode_ReturnEquipmentFound(string code)
		{
			try
			{
				DetailEquipDTO detailEquipDTO = apiEquipmentController.GetDetailEquipment(code);

				if (detailEquipDTO != null)
					output.WriteLine($"Equipment detail:\n{JsonConvert.SerializeObject(detailEquipDTO)}");
				else
					output.WriteLine($"Equipment code not found");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}




		[Theory]
		[MemberData(nameof(EquipmentControllerTestData.CreateEquipmentTestData), MemberType = typeof(EquipmentControllerTestData))]
		public async Task TestSaveEquipmentBySystemCode_ReturnStatusCode200(EquipmentNew equipmentNew)
		{
			try
			{
				var actionResult = await apiEquipmentController.SaveEquipment(equipmentNew);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);

				Assert.Equal(200, (int)response.StatusCode);
				output.WriteLine($"Status code: {(int)response.StatusCode}");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}






		[Theory]
		[MemberData(nameof(EquipmentControllerTestData.ImportEquipmentTestData), MemberType = typeof(EquipmentControllerTestData))]
		public async Task TestImportEquipmentBySystemCode_ReturnStatusCode200(List<EquipmentDTO> equipmentDTOs)
		{
			try
			{
				var actionResult = await apiEquipmentController.ImportEquipments(equipmentDTOs);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				Thread.Sleep(3000);

				Assert.Equal(200, (int)response.StatusCode);
				output.WriteLine($"Status code: {(int)response.StatusCode}");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}




		[Theory]
		[MemberData(nameof(EquipmentControllerTestData.UpdateEquipmentTestData), MemberType = typeof(EquipmentControllerTestData))]
		public async Task TestUpdateEquipmentBySystemCode_ReturnStatusCode200(int id, EquipmentDTO equipment)
		{
			try
			{
				var actionResult = await apiEquipmentController.UpdateEquipment(id, equipment);

				Thread.Sleep(3000);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);

				Assert.Equal(200, (int)response.StatusCode);
				output.WriteLine($"Status code: {(int)response.StatusCode}");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}
		#endregion
	}




	public static class EquipmentControllerTestData
	{
		public static IEnumerable<object[]> CreateEquipmentTestData()
		{
			EquipmentDTO e1 = new() { system_equipment_code = "E0123", equipment_name = "hoho" };
			EquipmentDTO e2 = new() { system_equipment_code = "E0456", equipment_name = "haha" };
			EquipmentDTO e3 = new() { system_equipment_code = "E0789", equipment_name = "hihi" };


			List<EquipComponentDTO> list1 = new()
			{
				new EquipComponentDTO() { equipment_name = "new new", standard_equipment_code = "E0101", system_equipment_code = "E1111" },
				new EquipComponentDTO() { equipment_name = "old", standard_equipment_code = "E0102", system_equipment_code = "E1112" }
			};

			List<EquipComponentDTO> list2 = new()
			{
				new EquipComponentDTO() { equipment_name = "very old", standard_equipment_code = "E0103", system_equipment_code = "E1113" },
				new EquipComponentDTO() { equipment_name = "null", standard_equipment_code = "E0104", system_equipment_code = "E1114" }
			};

			List<EquipComponentDTO> list3 = new()
			{
				new EquipComponentDTO() { equipment_name = "asd", standard_equipment_code = "E0105", system_equipment_code = "E1115" },
				new EquipComponentDTO() { equipment_name = "hí hí", standard_equipment_code = "E0106", system_equipment_code = "E1116" }
			};


			yield return new object[] { new EquipmentNew() { equip = e1, ListComponentEquips = list1, location_id = 1, img = null } };
			yield return new object[] { new EquipmentNew() { equip = e2, ListComponentEquips = list2, location_id = 1, img = null } };
			yield return new object[] { new EquipmentNew() { equip = e3, ListComponentEquips = list3, location_id = 1, img = null } };
		}




		public static IEnumerable<object[]> ImportEquipmentTestData()
		{
			List<EquipmentDTO> list1 = new()
			{

				new EquipmentDTO(){equipment_name="GSG9"},
				new EquipmentDTO(){equipment_name="GIGN"}
			};

			List<EquipmentDTO> list2 = new()
			{

				new EquipmentDTO(){ equipment_name="SAS"},
				new EquipmentDTO(){ equipment_name="SEAL"}
			};

			yield return new object[] { list1 };
			yield return new object[] { list2 };
		}



		public static IEnumerable<object[]> UpdateEquipmentTestData()
		{
			yield return new object[] { 0, new EquipmentDTO() { system_equipment_code = "E0010", equipment_name = "no non" } };
			yield return new object[] { 21, new EquipmentDTO() { system_equipment_code = "E0021", equipment_name = "hàng xịn" } };
			yield return new object[] { 22, new EquipmentDTO() { system_equipment_code = "E0022", equipment_name = "null null" } };
		}
	}
}
