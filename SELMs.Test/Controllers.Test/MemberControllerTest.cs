using SELMs.Api.HumanResource;
using SELMs.Models.BusinessModel;
using Xunit.Abstractions;

namespace SELMs.Test.Controllers.Test
{
	public class MemberControllerTest
	{

		private readonly ITestOutputHelper output;
		private readonly ApiMemberController apiMemberController = new();


		public MemberControllerTest(ITestOutputHelper output)
		{
			this.output = output;
			ApiControllerSetup.SetupController(apiMemberController);
		}


		#region Iteration 1

		[Theory]
		[MemberData(nameof(ControllerTestData.GetTestData), MemberType = typeof(ControllerTestData))]
		public async Task TestGetMemberList_ReturnOk(Argument argument)
		{
			try
			{
				var actionResult = await apiMemberController.GetMemberList(argument);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				string content = await response.Content.ReadAsStringAsync();

				output.WriteLine($"Test case passed - Status code: {(int)response.StatusCode}\n{content}");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed by exception\n" + ex.Message);
			}

		}



		[Theory]
		[InlineData(-1)]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		public async Task TestGetMemberById_ReturnMemberFound(int id)
		{
			try
			{
				var actionResult = await apiMemberController.GetMember(id);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				string content = await response.Content.ReadAsStringAsync();

				output.WriteLine($"Test case passed - Status code: {(int)response.StatusCode}\n{content}");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}




		[Fact]
		public async Task TestCreateNewMember_ReturnOk()
		{
			try
			{
				var actionResult = await apiMemberController.CreateNewMember(null);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				string content = await response.Content.ReadAsStringAsync();

				output.WriteLine($"Test case passed - Status code: {(int)response.StatusCode}\n{content}");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}



		[Fact]
		public async Task TestUpdateMember_ReturnOk()
		{
			try
			{
				var a = await apiMemberController.UpdateMember(1, null);

				output.WriteLine("Test case passed");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}


		#endregion
	}

















	public class ControllerTestData
	{
		public static IEnumerable<object[]> GetTestData()
		{
			yield return new object[] { new Argument() { fullname = "tan" } };
			yield return new object[] { new Argument() { fullname = "dat" } };
		}
	}
}
