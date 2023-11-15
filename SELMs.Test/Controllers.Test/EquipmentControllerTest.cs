using System.Web.Http.Results;
using Newtonsoft.Json;

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
		public async Task TestGetEquipmentById_ReturnEquipmentFound(int id)
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







		[Theory]
		[InlineData("E0000")]
		[InlineData("E0002")]
		[InlineData("E0004")]
		public void TestGetEquipmentBySystemCode_ReturnEquipmentFound(string code)
		{
			try
			{
				DetailEquipDTO detailEquipDTO = apiEquipmentController.GetDetailEquipment(code);

				if (detailEquipDTO != null)
					output.WriteLine($"Equipment detail:\n{JsonConvert.SerializeObject(detailEquipDTO)}");
				else
					output.WriteLine($"Equipment code not found");
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
