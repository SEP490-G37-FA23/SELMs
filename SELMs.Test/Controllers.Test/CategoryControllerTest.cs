﻿

using System.Web.Http.Results;

namespace SELMs.Test.Controllers.Test
{
	public class CategoryControllerTest
	{


		private readonly ITestOutputHelper output;
		private readonly ApiCategoryController apiCategoryController = new();


		public CategoryControllerTest(ITestOutputHelper output)
		{
			this.output = output;
			ApiControllerSetup.SetupController(apiCategoryController);
		}




		#region Iteration 2

		[Fact]
		public async Task TestGetCategoryList_ReturnCategoryList()
		{
			var actionResult = await apiCategoryController.GetCategoryList();
			var response = await actionResult.ExecuteAsync(CancellationToken.None);
			string content = await response.Content.ReadAsStringAsync();


			var list = JsonConvert.DeserializeObject<List<CategoryDTO>>(content);

			foreach (var item in list)
				output.WriteLine(JsonConvert.SerializeObject(item));
		}






		[Theory]
		[MemberData(nameof(CategoryControllerTestData.SeacrchCategoryTestData), MemberType = typeof(CategoryControllerTestData))]
		public async Task TestSearchCategory_ReturnCategoryFound(bool expectedException, Argument argument)
		{
			var actionResult = await apiCategoryController.GetCategories(argument);
			var response = await actionResult.ExecuteAsync(CancellationToken.None);
			string content = await response.Content.ReadAsStringAsync();


			if (expectedException)
				Assert.IsType<BadRequestErrorMessageResult>(actionResult);


			output.WriteLine(content);
		}




		[Theory]
		[InlineData(true, 0)]
		[InlineData(false, 1)]
		[InlineData(false, 2)]
		public async Task TestGetCategoryById_ReturnCategoryFound(bool expectedException, int id)
		{
			var actionResult = await apiCategoryController.GetCategory(id);

			if (expectedException)
			{
				Assert.Null(actionResult);
				output.WriteLine("Category not found");
			}
			else
			{
				output.WriteLine(JsonConvert.SerializeObject(actionResult));
			}
		}





		[Theory]
		[MemberData(nameof(CategoryControllerTestData.CreateCategoryTestData), MemberType = typeof(CategoryControllerTestData))]
		public async Task TestCreateCategory_ReturnNothing(CategoryRequest categoryRequest)
		{
			try
			{
				var actionResult = await apiCategoryController.SaveCategory(categoryRequest);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);

				output.WriteLine($"Test case passed - Status code: {(int)response.StatusCode}");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}





		[Theory]
		[MemberData(nameof(CategoryControllerTestData.UpdateCategoryTestData), MemberType = typeof(CategoryControllerTestData))]
		public async Task TestUpdateCategory_ReturnNothing(int id, CategoryDTO categoryDTO)
		{
			try
			{
				var actionResult = await apiCategoryController.UpdateCategory(id, categoryDTO);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);

				output.WriteLine($"Test case passed - Status code: {(int)response.StatusCode}");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}

		#endregion
	}












	public static class CategoryControllerTestData
	{


		// Use for search category
		public static IEnumerable<object[]> SeacrchCategoryTestData()
		{
			yield return new object[] { true, null! };
			yield return new object[] { true, new Argument() { } };
			yield return new object[] { false, new Argument() { text = "" } };
			yield return new object[] { false, new Argument() { text = "GG" } };
			yield return new object[] { false, new Argument() { text = "Khác" } };
		}



		// Use for create category
		public static IEnumerable<object[]> CreateCategoryTestData()
		{

			var cate1 = new CategoryDTO() { category_code = "S1", category_name = "yes" };
			var cate2 = new CategoryDTO() { category_code = "S2", category_name = "no" };
			var cate3 = new CategoryDTO() { category_code = "S3", category_name = "huh" };

			var list1 = new List<EquipmentDTO>()
			{
				new EquipmentDTO(){equipment_name="1234"},
				new EquipmentDTO(){equipment_name="5678"},

			};

			var list2 = new List<EquipmentDTO>()
			{
				new EquipmentDTO(){equipment_name="eq1"},
				new EquipmentDTO(){equipment_name="eq2"},

			};

			var list3 = new List<EquipmentDTO>()
			{
				new EquipmentDTO(){equipment_name="hoho"},
				new EquipmentDTO(){equipment_name="ok"},

			};

			yield return new object[] { new CategoryRequest { category = cate1, equipments = null } };
			yield return new object[] { new CategoryRequest { category = cate2, equipments = null } };
			yield return new object[] { new CategoryRequest { category = cate3, equipments = null } };
		}



		// Use for update category
		public static IEnumerable<object[]> UpdateCategoryTestData()
		{
			yield return new object[] { 2, new CategoryDTO() { category_name = "tên mới" } };
			yield return new object[] { 3, new CategoryDTO() { category_name = "Ko có tên" } };
			yield return new object[] { 5, new CategoryDTO() { category_name = "Haha123" } };
		}
	}
}
