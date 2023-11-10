﻿

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
			try
			{
				var actionResult = await apiCategoryController.GetCategoryList();
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
		[MemberData(nameof(CategoryControllerTestData.SeacrchCategoryTestData), MemberType = typeof(CategoryControllerTestData))]
		public async Task TestSearchCategory_ReturnCategoryFound(Argument argument)
		{
			try
			{
				var actionResult = await apiCategoryController.SearchCategories(argument);
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
		[InlineData(-1)]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		public async Task TestGetCategoryById_ReturnCategoryFound(int id)
		{
			try
			{
				var actionResult = await apiCategoryController.GetCategory(id);
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
			yield return new object[] { new Argument() { username = "Laptop" } };
			yield return new object[] { new Argument() { username = "Thiết bị đo" } };
			yield return new object[] { new Argument() { username = "aaaaaa" } };
		}



		// Use for create category
		public static IEnumerable<object[]> CreateCategoryTestData()
		{
			yield return new object[] { };
			yield return new object[] { };
			yield return new object[] { };
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