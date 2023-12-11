namespace SELMs.Test.Services.Test
{
	public class EquipmentImportServiceTest
	{
		private readonly ITestOutputHelper output;
		private readonly IEquipmentImportService equipmentImportService = new EquipmentImportService();


		public EquipmentImportServiceTest(ITestOutputHelper output)
		{
			this.output = output;
		}



		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		public async Task TestGetApplicationById_ReturnApplication(int id)
		{
			var app = await equipmentImportService.GetApplication(id);

			if (app == null)
				output.WriteLine("Appliaction not found");
			else
				output.WriteLine(JsonConvert.SerializeObject(app));
		}




		[Theory]
		[MemberData(nameof(EquipmentImportServiceTestData.CreateApplicationTestData), MemberType = typeof(EquipmentImportServiceTestData))]
		public async Task TestCreateApplication_ReturnNoException(Equipment_Import_Application application, List<Equipment_Import_Application_Detail> applicationDetails)
		{
			try
			{
				await equipmentImportService.SaveApplication(application, applicationDetails);
				output.WriteLine("Test case passed");
			}
			catch (Exception ex)
			{

				Assert.Fail(ex.Message);
			}
		}







		[Theory]
		[MemberData(nameof(EquipmentImportServiceTestData.UpdateApplicationTestData), MemberType = typeof(EquipmentImportServiceTestData))]
		public async Task TestUpdateApplication_ReturnNoException(int id, Equipment_Import_Application application, List<Equipment_Import_Application_Detail> applicationDetails)
		{
			try
			{
				await equipmentImportService.UpdateApplication(id, application, applicationDetails);
				Thread.Sleep(2000);
				output.WriteLine("Test case passed");
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}
	}


	internal static class EquipmentImportServiceTestData
	{
		public static IEnumerable<object[]> CreateApplicationTestData()
		{

			var a1 = new Equipment_Import_Application() { supplier = "camo", created_by = "SM" };
			var list1 = new List<Equipment_Import_Application_Detail>()
			{
				new Equipment_Import_Application_Detail(){system_equipment_code="E1010",equipment_name="as"},
				new Equipment_Import_Application_Detail(){system_equipment_code="E1011",equipment_name="dd"}
			};


			var a2 = new Equipment_Import_Application() { supplier = "Norad", created_by = "Lab" };
			var list2 = new List<Equipment_Import_Application_Detail>()
			{
				new Equipment_Import_Application_Detail(){system_equipment_code="E1012",equipment_name="Gấu nhồi bông"},
				new Equipment_Import_Application_Detail(){system_equipment_code="E1013",equipment_name="haha123"}
			};


			yield return new object[] { a1, list1 };
			yield return new object[] { a2, list2 };
		}



		public static IEnumerable<object[]> UpdateApplicationTestData()
		{

			var a1 = new Equipment_Import_Application() { ei_application_code = "EIA202311254", created_by = "Aladeen" };
			var list1 = new List<Equipment_Import_Application_Detail>()
			{
				new Equipment_Import_Application_Detail(){application_detail_id = 9,system_equipment_code= "E3043",ei_application_code="EIA202311255",equipment_name="lego"},
				new Equipment_Import_Application_Detail(){application_detail_id = 10,system_equipment_code= "E1061",ei_application_code="EIA202311255",equipment_name="ship"},
			};


			var a2 = new Equipment_Import_Application() { ei_application_code = "EIA202311254", created_by = "christopher nolan" };
			var list2 = new List<Equipment_Import_Application_Detail>()
			{
				new Equipment_Import_Application_Detail(){application_detail_id = 13,system_equipment_code= "E3066",ei_application_code="EIA202311259",equipment_name="MG34"},
				new Equipment_Import_Application_Detail(){application_detail_id = 14,system_equipment_code= "E1010",ei_application_code="EIA202311260",equipment_name="FG-42"}
			};

			yield return new object[] { 4, a1, list1 };
			yield return new object[] { 7, a2, list2 };
		}
	}
}
