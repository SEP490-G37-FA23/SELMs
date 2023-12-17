using System.Data.Entity;

namespace SELMs.Test.Services.Test
{
	public class CategoryServiceTest
	{

		private readonly ITestOutputHelper output;
		private readonly SELMsContext sELMsContext = new();
		private readonly ICategoryService categoryService = new CategoryService();

		public CategoryServiceTest(ITestOutputHelper output)
		{
			this.output = output;
		}



		#region Iteration 2



		[Theory]
		[MemberData(nameof(CategoryServiceTestData.SaveCategoryTestData), MemberType = typeof(CategoryServiceTestData))]
		public async Task TestCategory_ReturnNoException(Category category, List<StandardEquipmentDTO> equipments)
		{
			try
			{
				await categoryService.SaveCategory(category, equipments);

				var cat = await sELMsContext.Categories.FirstOrDefaultAsync(c => c.category_name.Equals(category.category_name));


				Assert.NotNull(cat);
				Assert.Equal(category.category_code, cat.category_code);
				Assert.Equal(category.category_name, cat.category_name);

				var list = await sELMsContext.Equipments
					.Where(e => e.category_code.Equals(category.category_code))
					.ToListAsync();


				for (int i = 0; i < equipments.Count; i++)
					Assert.Equal(equipments[i].equipment_name, list[i].equipment_name);


				output.WriteLine("Test case passed");
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}



		[Theory]
		[MemberData(nameof(CategoryServiceTestData.UpdateCategoryTestData), MemberType = typeof(CategoryServiceTestData))]
		public async Task TestUpdateCategory_ReturnNoException(int id, Category category)
		{
			await categoryService.UpdateCategory(id, category);

			Thread.Sleep(500);

			var cat = await sELMsContext.Categories.FirstOrDefaultAsync(c => c.category_id == id);

			if (cat != null)
			{
				Assert.Equal(category.category_name, cat.category_name);
				output.WriteLine("Update successfull");
			}
		}
		#endregion
	}








	public static class CategoryServiceTestData
	{
		public static IEnumerable<object[]> SaveCategoryTestData()
		{
			var cate1 = new Category() { category_code = "AS", category_name = "anakin skywalker" };
			var list1 = new List<StandardEquipmentDTO>
			{
				new StandardEquipmentDTO() { equipment_name = "" },
				new StandardEquipmentDTO() { equipment_name = "hello" }
			};


			var cate2 = new Category() { category_code = "OK", category_name = "obiwan kenobi" };
			var list2 = new List<StandardEquipmentDTO>();


			yield return new object[] { cate1, list1 };
			//yield return new object[] { cate2, list2 };
		}


		public static IEnumerable<object[]> UpdateCategoryTestData()
		{
			yield return new object[] { 0, new Category() { category_name = "new category" } };
			yield return new object[] { 1, new Category() { category_name = "Vi mạch" } };
			yield return new object[] { 2, new Category() { category_name = "Chip bán dẫn" } };
		}



	}
}
