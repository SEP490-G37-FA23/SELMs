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
		public async Task TestGetCategoryById_ReturnCategoryFound(int id)
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
		#endregion
	}




	public static class EquipmentControllerTestData
	{

	}
}
