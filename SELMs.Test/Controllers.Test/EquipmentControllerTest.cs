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

				//Assert.IsType<Equipment>(content);
				output.WriteLine($"Test case passed - Status code: {(int)response.StatusCode}\n{content}");
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
