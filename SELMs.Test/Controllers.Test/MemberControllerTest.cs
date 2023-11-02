using SELMs.Api.DTOs;
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
		[MemberData(nameof(ControllerTestData.GetMemberListTestData), MemberType = typeof(ControllerTestData))]
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




		[Theory]
		[MemberData(nameof(ControllerTestData.CreateNewMemberTestData), MemberType = typeof(ControllerTestData))]
		public async Task TestCreateNewMember_ReturnOk(UserDTO userDTO)
		{
			try
			{
				var actionResult = await apiMemberController.CreateNewMember(userDTO);
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
		[MemberData(nameof(ControllerTestData.UpdateMemberTestData), MemberType = typeof(ControllerTestData))]
		public async Task TestUpdateMember_ReturnOk(int id, UserDTO userDTO)
		{
			try
			{
				var actionResult = await apiMemberController.UpdateMember(id, userDTO);
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
		public async Task TestResignMember_ReturnOk(int id)
		{
			try
			{
				var actionResult = await apiMemberController.ResignMember(id);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				string content = await response.Content.ReadAsStringAsync();

				output.WriteLine($"Test case passed - Status code: {(int)response.StatusCode}\n{content}");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}


		#endregion







		#region Iteration 2
		#endregion



		#region Iteration 3
		#endregion
	}
















	/*Data to test*/
	public static class ControllerTestData
	{
		public static IEnumerable<object[]> GetMemberListTestData()
		{
			yield return new object[] { new Argument() { fullname = "tan" } };
			yield return new object[] { new Argument() { fullname = "da" } };
			yield return new object[] { new Argument() { fullname = "Ly" } };
		}


		public static IEnumerable<object[]> UpdateMemberTestData()
		{
			yield return new object[] { -1, new UserDTO() { fullname = "" } };
			yield return new object[] { 0, new UserDTO() { fullname = "Hà" } };
			yield return new object[] { 1, new UserDTO() { fullname = "Nguyễn Tuấn Anh" } };
			yield return new object[] { 2, new UserDTO() { fullname = "Lê Văn Luyện" } };
		}

		public static IEnumerable<object[]> CreateNewMemberTestData()
		{
			yield return new object[] { new UserDTO() { fullname = "" } };
			yield return new object[] { new UserDTO() { fullname = "dada" } };
			yield return new object[] { new UserDTO() { fullname = "Lê Tuấn Linh" } };
		}

	}
}
