using SELMs.Api.HumanResource;
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
			//AppDomain.CurrentDomain.SetupInformation.ConfigurationFile = "G:\\FPT\\Semester_9\\Capstone\\Project\\SELMs.Test\\App.config";
		}


		#region Iteration 1

		[Fact]
		public async Task TestGetMemberList_ReturnOk()
		{
			try
			{
				var actionResult = await apiMemberController.get();
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				var content = await response.Content.ReadAsStringAsync();

				output.WriteLine($"Test case passed - Status code: {(int)response.StatusCode}\n{content}");

			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}

		}



		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		public async Task TestGetMemberById_ReturnMemberFound(int id)
		{
			try
			{
				var actionResult = await apiMemberController.GetMember(id);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				var content = await response.Content.ReadAsStringAsync();

				output.WriteLine($"Test case passed - Status code: {((int)response.StatusCode)}\n{content}");

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
				var a = await apiMemberController.CreateNewMember(null);

				output.WriteLine("Test case passed");
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
}
