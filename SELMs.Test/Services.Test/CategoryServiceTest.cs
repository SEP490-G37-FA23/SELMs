namespace SELMs.Test.Services.Test
{
	public class CategoryServiceTest
	{

		private readonly ITestOutputHelper output;
		private readonly ICategoryService categoryService = new CategoryService();

		public CategoryServiceTest(ITestOutputHelper output)
		{
			this.output = output;
		}



		#region Iteration 2



		[Theory]
		[MemberData(nameof(CategoryServiceTestData.SaveCategoryTestData), MemberType = typeof(CategoryServiceTestData))]
		public async Task TestSaveMember_ReturnNothing(Category category, List<Equipment> equipments)
		{
			try
			{
				await categoryService.SaveCategory(category, null);
				output.WriteLine("Test case passed");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}



		[Theory]
		[MemberData(nameof(CategoryServiceTestData.UpdateCategoryTestData), MemberType = typeof(CategoryServiceTestData))]
		public async Task TestUpdateCategory_ReturnNothing(int id, Category category)
		{
			try
			{
				await categoryService.UpdateCategory(id, category);
				output.WriteLine("Test case passed");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}
		#endregion
	}








	public static class CategoryServiceTestData
	{
		public static IEnumerable<object[]> SaveCategoryTestData()
		{

			var cate1 = new Category() { category_code = "AS", category_name = "anakin skywalker" };
			var list1 = new List<Equipment>
			{
				new Equipment() { equipment_name = "yo" },
				new Equipment() { equipment_name = "yo1" }
			};


			var cate2 = new Category() { category_code = "OK", category_name = "obiwan kenobi" };
			var list2 = new List<Equipment>
			{
				new Equipment() { equipment_name = "yo2" },
				new Equipment() { equipment_name = "yo3" }
			};

			yield return new object[] { cate1, list1 };
			yield return new object[] { cate2, list2 };
		}


		public static IEnumerable<object[]> UpdateCategoryTestData()
		{
			yield return new object[] { 0, new Category() { category_name = "heehee", desciption = "dunno" } };
			yield return new object[] { 6, new Category() { category_name = "katana", desciption = null } };
		}



	}
}
