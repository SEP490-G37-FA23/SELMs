using System.Data.Entity;

namespace SELMs.Test.Repositories.Test
{
	public class CategoryRepositoryTest
	{
		private readonly ITestOutputHelper output;
		private readonly ICategoryRepository categoryRepository = new CategoryRepository();

		public CategoryRepositoryTest(ITestOutputHelper output)
		{
			this.output = output;
		}



		#region Iteration 2

		[Fact]
		public async Task TestGetCategoryList_ReturnCategoryList()
		{
			var list = await categoryRepository.GetCategoryList();

			foreach (var item in list)
				output.WriteLine(JsonConvert.SerializeObject(item));

			Assert.True(list is List<Category> { Count: > 0 });
		}





		[Theory]
		[MemberData(nameof(CategoryRepositoryTestData.SearchCategoryListTestData), MemberType = typeof(CategoryRepositoryTestData))]
		public async Task TestSearchCategory_ReturnCategoryListFound(Argument argument)
		{
			var categoryListFound = await categoryRepository.SearchCategories(argument);


			Assert.NotNull(categoryListFound);


			if (categoryListFound is List<dynamic> { Count: > 0 })
				foreach (var item in categoryListFound)
					output.WriteLine(JsonConvert.SerializeObject(item));

			else
				output.WriteLine("Categories not found");
		}





		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(6)]
		[InlineData(int.MaxValue)]
		public void TestGetCategoryById_ReturnCategoryFound(int id)
		{
			var category = categoryRepository.GetCategory(id);

			if (category == null)
				output.WriteLine("Category not found");

			else
				output.WriteLine(JsonConvert.SerializeObject(category));
		}






		[Theory]
		[MemberData(nameof(CategoryRepositoryTestData.CreateCategoryTestData), MemberType = typeof(CategoryRepositoryTestData))]
		public async Task TestCreateCategory_ReturnCreatedCategory(string expectedCategoryName, Category category)
		{
			categoryRepository.SaveCategory(category);

			SELMsContext sELMsContext = new();

			Category categoryFound = await sELMsContext.Categories
				.FirstOrDefaultAsync(c => c.category_name.Equals(expectedCategoryName) && c.is_active == true);

			Assert.Equal(expectedCategoryName, categoryFound.category_name);
			output.WriteLine(JsonConvert.SerializeObject(categoryFound));
		}



		[Theory]
		[MemberData(nameof(CategoryRepositoryTestData.UpdateCategoryTestData), MemberType = typeof(CategoryRepositoryTestData))]
		public void TestUpdateCategory_ReturnCreatedCategory(Category category)
		{
			try
			{
				categoryRepository.UpdateCategory(category);
				var updatedCategory = categoryRepository.GetCategory(category.category_id);

				Assert.Equal((updatedCategory as Category)?.category_name, category.category_name);
				output.WriteLine(JsonConvert.SerializeObject(updatedCategory));
			}
			catch (Exception ex)
			{
				Assert.IsType<ArgumentNullException>(ex);
				output.WriteLine(ex.Message);
			}
		}



		#endregion
	}









	public static class CategoryRepositoryTestData
	{
		// not test
		public static IEnumerable<object[]> SearchCategoryListTestData()
		{
			// text is category name
			yield return new object[] { new Argument() { text = "Laptop" } };
			yield return new object[] { new Argument() { text = "" } };
			yield return new object[] { new Argument() { text = "kaka" } };
		}




		public static IEnumerable<object[]> CreateCategoryTestData()
		{
			yield return new object[] { "new category", new Category() { category_name = "new category", is_active = true } };
			yield return new object[] { "", new Category() { category_name = "", is_active = true } };
		}



		public static IEnumerable<object[]> UpdateCategoryTestData()
		{
			yield return new object[] { new Category() { category_id = 0, category_name = "haha" } };
			//yield return new object[] { new Category() { category_id = 7, category_name = "you" } };
			//yield return new object[] { new Category() { category_id = 8, category_name = "đổi mới" } };
		}


	}
}
