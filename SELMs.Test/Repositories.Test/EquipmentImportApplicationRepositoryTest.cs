﻿using System.Collections;

namespace SELMs.Test.Repositories.Test
{
	public class EquipmentImportApplicationRepositoryTest
	{
		private readonly ITestOutputHelper output;
		private readonly IEquipmentImportApplicationRepository repository = new EquipmentImportApplicationRepository();

		public EquipmentImportApplicationRepositoryTest(ITestOutputHelper output)
		{
			this.output = output;
		}




		#region Application

		[Fact]
		public async Task TestGetApplicationList_ReturnList()
		{
			var list = await repository.GetApplicationList();

			foreach (var item in list)
				output.WriteLine(JsonConvert.SerializeObject(item));

			Assert.True(list is IList { Count: > 0 });
		}


		[Fact]
		public async Task TestGetApplicationListByName_ReturnList()
		{
			var list = await repository.GetApplicationList(null);

			foreach (var item in list)
				output.WriteLine(JsonConvert.SerializeObject(item));

			Assert.True(list is IList { Count: > 0 });
		}








		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(3)]
		public void TestGetApplicationById_ReturnApplication(int id)
		{
			var app = repository.GetApplication(id);

			if (app != null)
				output.WriteLine(JsonConvert.SerializeObject(app));
			else
				output.WriteLine("Application not found");
		}




		[Fact]
		public void TestGetLastDailyApplication_ReturnList()
		{
			var app = repository.GetLastDailyApplication();

			if (app != null)
				output.WriteLine(JsonConvert.SerializeObject(app));
			else
				output.WriteLine("Application with last date not found");
		}



		[Theory]
		[MemberData(nameof(EquipmentImportApplicationRepositoryTestData.CreateApplicationTestData), MemberType = typeof(EquipmentImportApplicationRepositoryTestData))]
		public void TestCreateApplication_ReturnCreatedApplication(Equipment_Import_Application application)
		{
			try
			{
				var app = repository.SaveApplication(application);
				output.WriteLine(JsonConvert.SerializeObject(app));
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}



		[Theory]
		[MemberData(nameof(EquipmentImportApplicationRepositoryTestData.UpdateApplicationTestData), MemberType = typeof(EquipmentImportApplicationRepositoryTestData))]
		public void TestUpdateApplication_ReturnNoException(string expectedCreatedByName, Equipment_Import_Application application)
		{
			try
			{
				repository.UpdateApplication(application);
				Thread.Sleep(1000);
				Equipment_Import_Application app = repository.GetApplication(application.application_id);

				Assert.Equal(expectedCreatedByName, app.created_by);
				output.WriteLine("Test case passed");
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}



		[Theory]
		[InlineData(0)]
		[InlineData(3)]
		public void TestDeleteApplicationById_ReturnNoException(int id)
		{
			try
			{
				repository.DeleteApplication(id);
				output.WriteLine("Test case passed");
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}
		#endregion






		#region Application Detail
		[Fact]
		public async Task TestGetApplicationDetailList_ReturnList()
		{
			var list = await repository.GetApplicationDetailList();

			foreach (var item in list)
				output.WriteLine(JsonConvert.SerializeObject(item));

			Assert.True(list is IList { Count: > 0 });
		}





		[Theory]
		[MemberData(nameof(EquipmentImportApplicationRepositoryTestData.CreateApplicationDetailTestData), MemberType = typeof(EquipmentImportApplicationRepositoryTestData))]
		public void TestCreateApplicationDetail_ReturnCreatedApplicationDetail(Equipment_Import_Application_Detail applicationDetail)
		{
			try
			{
				var app = repository.SaveApplicationDetail(applicationDetail);
				output.WriteLine(JsonConvert.SerializeObject(app));
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}



		[Theory]
		[MemberData(nameof(EquipmentImportApplicationRepositoryTestData.CreateApplicationDetailsTestData), MemberType = typeof(EquipmentImportApplicationRepositoryTestData))]
		public void TestCreateApplicationDetails_ReturnCreatedApplicationDetail(List<Equipment_Import_Application_Detail> applicationDetails)
		{
			try
			{
				IList list = repository.SaveApplicationDetails(applicationDetails);

				foreach (var item in list)
					output.WriteLine(JsonConvert.SerializeObject(item));
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}




		[Theory]
		[MemberData(nameof(EquipmentImportApplicationRepositoryTestData.UpdateApplicationDetailTestData), MemberType = typeof(EquipmentImportApplicationRepositoryTestData))]
		public void TestUpdateApplicationDetails_ReturnNoException(string expected, Equipment_Import_Application_Detail applicationDetail)
		{
			try
			{
				repository.UpdateApplicationDetail(applicationDetail);
				Thread.Sleep(1000);
				Equipment_Import_Application_Detail app = repository.GetApplicationDetail(applicationDetail.application_detail_id);

				Assert.Equal(expected, app.equipment_name);
				output.WriteLine("Test case passed");
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}



		[Theory]
		[InlineData("")]
		[InlineData("EIA202311")]
		[InlineData("EIA202311251")]
		public async Task TestGetApplicationDetailListByCode_ReturnApplications(string applicationCode)
		{
			var app = await repository.GetApplicationDetailList(applicationCode);

			if (app != null)
				output.WriteLine(JsonConvert.SerializeObject(app));
			else
				output.WriteLine("Application not found");
		}










		[Theory]
		[InlineData(0)]
		[InlineData(3)]
		[InlineData(5)]
		public void TestGetApplicationDetailById_ReturnApplicationDetail(int id)
		{
			var app = repository.GetApplicationDetail(id);

			if (app != null)
				output.WriteLine(JsonConvert.SerializeObject(app));
			else
				output.WriteLine("Application detail not found");
		}






		[Theory]
		[InlineData(0)]
		[InlineData(3)]
		[InlineData(5)]
		public void TestDeleteApplicationDetailById_ReturnNoException(int id)
		{
			try
			{
				repository.DeleteApplicationDetail(id);
				output.WriteLine("Test case passed");
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		#endregion
	}



	internal static class EquipmentImportApplicationRepositoryTestData
	{
		public static IEnumerable<object[]> CreateApplicationTestData()
		{
			var a = new Equipment_Import_Application() { ei_application_code = "EIA202311254", created_by = "Lockheed Martin" };
			var b = new Equipment_Import_Application() { ei_application_code = "EIA202311255", created_by = "Mikoyan" };
			var c = new Equipment_Import_Application() { ei_application_code = "EIA202311256", created_by = "Avtomat Kalashnikov" };

			yield return new object[] { a };
			yield return new object[] { b };
			yield return new object[] { c };
		}


		public static IEnumerable<object[]> UpdateApplicationTestData()
		{
			var a = new Equipment_Import_Application() { application_id = 1, ei_application_code = "EIA20231125", created_by = "Porsche" };
			var b = new Equipment_Import_Application() { application_id = 2, ei_application_code = "EIA202311251", created_by = "Misubitshi" };

			yield return new object[] { "Porsche", a };
			yield return new object[] { "Misubitshi", b };
		}



		public static IEnumerable<object[]> CreateApplicationDetailTestData()
		{
			var a = new Equipment_Import_Application_Detail() { ei_application_code = "EIA202311253", system_equipment_code = "E1021" };
			var b = new Equipment_Import_Application_Detail() { ei_application_code = "EIA202311254", system_equipment_code = "E2032" };
			var c = new Equipment_Import_Application_Detail() { ei_application_code = "EIA202311255", system_equipment_code = "E3043" };

			yield return new object[] { a };
			yield return new object[] { b };
			yield return new object[] { c };
		}



		public static IEnumerable<object[]> CreateApplicationDetailsTestData()
		{
			var a = new Equipment_Import_Application_Detail() { ei_application_code = "EIA202311256", system_equipment_code = "E1061" };
			var b = new Equipment_Import_Application_Detail() { ei_application_code = "EIA202311257", system_equipment_code = "E2062" };
			var c = new Equipment_Import_Application_Detail() { ei_application_code = "EIA202311258", system_equipment_code = "E3063" };


			var d = new Equipment_Import_Application_Detail() { ei_application_code = "EIA202311259", system_equipment_code = "E1064" };
			var e = new Equipment_Import_Application_Detail() { ei_application_code = "EIA202311260", system_equipment_code = "E2065" };
			var f = new Equipment_Import_Application_Detail() { ei_application_code = "EIA202311261", system_equipment_code = "E3066" };

			List<Equipment_Import_Application_Detail> list1 = new() { a, b, c };
			List<Equipment_Import_Application_Detail> list2 = new() { d, e, f };

			yield return new object[] { list1 };
			yield return new object[] { list2 };
		}


		public static IEnumerable<object[]> UpdateApplicationDetailTestData()
		{
			var a = new Equipment_Import_Application_Detail() { application_detail_id = 13, ei_application_code = "EIA202311259", system_equipment_code = "E1064", equipment_name = "revolver" };
			var b = new Equipment_Import_Application_Detail() { application_detail_id = 15, ei_application_code = "EIA202311261", system_equipment_code = "E3066", equipment_name = "missle" };

			yield return new object[] { "revolver", a };
			yield return new object[] { "missle", b };
		}
	}
}
