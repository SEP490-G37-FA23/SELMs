namespace SELMs.Test.Controllers.Test
{
	public class EquipmentImportControllerTest
	{
		private readonly ITestOutputHelper output;
		private readonly ApiEquipmentImportController apiEquipmentImportController = new();


		public EquipmentImportControllerTest(ITestOutputHelper output)
		{
			this.output = output;
			ApiControllerSetup.SetupController(apiEquipmentImportController);
		}




		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(3)]
		public async Task TestGetApplicationById_ReturnApplication(int id)
		{
			try
			{
				var actionResult = await apiEquipmentImportController.GetApplication(id);
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
		[MemberData(nameof(EquipmentImportControllerTestData.CreateApplicationTestData), MemberType = typeof(EquipmentImportControllerTestData))]
		public async Task TestCreateApplication_ReturnStatusCode200(EquipmentImportApplicationRequest applicationRequest)
		{
			try
			{
				var actionResult = await apiEquipmentImportController.SaveApplication(applicationRequest);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				output.WriteLine($"Test case passed - Status code: {(int)response.StatusCode}");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}







		[Theory]
		[MemberData(nameof(EquipmentImportControllerTestData.UpdateApplicationTestData), MemberType = typeof(EquipmentImportControllerTestData))]
		public async Task TestUpdateApplication_ReturnStatusCode200(int id, EquipmentImportApplicationRequest applicationRequest)
		{
			try
			{
				var actionResult = await apiEquipmentImportController.UpdateApplication(id, applicationRequest);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				output.WriteLine($"Test case passed - Status code: {(int)response.StatusCode}");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}
	}



	internal static class EquipmentImportControllerTestData
	{

		public static IEnumerable<object[]> CreateApplicationTestData()
		{

			var a1 = new EquipmentImportApplicationDTO() { supplier = "CMC", created_by = "haha123" };
			var list1 = new List<EquipmentImportApplicationDetailDTO>()
			{
				new EquipmentImportApplicationDetailDTO(){system_equipment_code="E0101",equipment_name="shotgun"},
				new EquipmentImportApplicationDetailDTO(){system_equipment_code="E0102",equipment_name="rifle"}
			};


			var a2 = new EquipmentImportApplicationDTO() { supplier = "FPT", created_by = "outsource" };
			var list2 = new List<EquipmentImportApplicationDetailDTO>()
			{
				new EquipmentImportApplicationDetailDTO(){system_equipment_code="E0103",equipment_name="MG"},
				new EquipmentImportApplicationDetailDTO(){system_equipment_code="E0104",equipment_name="pistol"}
			};


			var a3 = new EquipmentImportApplicationDTO() { supplier = "MISA", created_by = "huh" };
			var list3 = new List<EquipmentImportApplicationDetailDTO>()
			{
				new EquipmentImportApplicationDetailDTO(){system_equipment_code="E0105",equipment_name="sniper"},
				new EquipmentImportApplicationDetailDTO(){system_equipment_code="E0106",equipment_name="RPG"}
			};


			yield return new object[] { new EquipmentImportApplicationRequest() { application = a1, application_details = list1 } };
			yield return new object[] { new EquipmentImportApplicationRequest() { application = a2, application_details = list2 } };
			yield return new object[] { new EquipmentImportApplicationRequest() { application = a3, application_details = list3 } };
		}



		public static IEnumerable<object[]> UpdateApplicationTestData()
		{

			var a1 = new EquipmentImportApplicationDTO() { created_by = "Jim Carry" };
			var list1 = new List<EquipmentImportApplicationDetailDTO>()
			{
				new EquipmentImportApplicationDetailDTO(){application_detail_id = 0,equipment_name="rifle"},
				new EquipmentImportApplicationDetailDTO(){application_detail_id = 1,equipment_name=""},
			};


			var a2 = new EquipmentImportApplicationDTO() { ei_application_code = "EIA202311251", created_by = "Obama" };
			var list2 = new List<EquipmentImportApplicationDetailDTO>()
			{
				new EquipmentImportApplicationDetailDTO(){application_detail_id = 1,ei_application_code="EIA20231125",equipment_name="P90"},
				new EquipmentImportApplicationDetailDTO(){application_detail_id = 3,ei_application_code="EIA202311251",equipment_name="MG42"}
			};

			yield return new object[] { 0, new EquipmentImportApplicationRequest() { application = a1, application_details = list1 } };
			yield return new object[] { 1, new EquipmentImportApplicationRequest() { application_details = list2 } };
			yield return new object[] { 2, new EquipmentImportApplicationRequest() { application = a2, application_details = list2 } };
		}
	}
}
