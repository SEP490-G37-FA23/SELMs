using System.Collections;
using System.Web.Http.Results;
using SELMs.Api.HumanResource;


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
		[MemberData(nameof(MemberControllerTestData.GetMemberListTestData), MemberType = typeof(MemberControllerTestData))]
		public async Task TestGetMemberList_ReturnMemberList(bool expectedException, Argument argument)
		{
			var actionResult = await apiMemberController.GetMemberList(argument);
			var response = await actionResult.ExecuteAsync(CancellationToken.None);
			string content = await response.Content.ReadAsStringAsync();

			if (expectedException)
			{
				Assert.Equal(400, (int)response.StatusCode);
				Assert.IsType<BadRequestErrorMessageResult>(actionResult);
				output.WriteLine(content);
			}
			else
			{
				Assert.Equal(200, (int)response.StatusCode);
				IList list = JsonConvert.DeserializeObject<IList>(content);

				foreach (var item in list)
					output.WriteLine(JsonConvert.SerializeObject(item));
			}
		}



		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		public async Task TestGetMemberById_ReturnMemberFound(int id)
		{
			var actionResult = await apiMemberController.GetMember(id);
			var response = await actionResult.ExecuteAsync(CancellationToken.None);
			string content = await response.Content.ReadAsStringAsync();


			Assert.Equal(200, (int)response.StatusCode);
			output.WriteLine(content);
		}




		[Theory]
		[MemberData(nameof(MemberControllerTestData.CreateNewMemberTestData), MemberType = typeof(MemberControllerTestData))]
		public async Task TestCreateNewMember_ReturnOk(bool expectedException, UserDTO userDTO)
		{
			var actionResult = await apiMemberController.CreateNewMember(userDTO);
			var response = await actionResult.ExecuteAsync(CancellationToken.None);


			if (expectedException)
			{
				Assert.Equal(400, (int)response.StatusCode);
				Assert.IsType<BadRequestErrorMessageResult>(actionResult);
				string content = await response.Content.ReadAsStringAsync();
				output.WriteLine(content);
			}
			else
			{
				Assert.Equal(200, (int)response.StatusCode);
				output.WriteLine("Status code: " + (int)response.StatusCode);
			}
		}



		[Theory]
		[MemberData(nameof(MemberControllerTestData.UpdateMemberTestData), MemberType = typeof(MemberControllerTestData))]
		public async Task TestUpdateMember_ReturnOk(bool expectedException, int id, UserDTO userDTO)
		{
			var actionResult = await apiMemberController.UpdateMember(id, userDTO);
			var response = await actionResult.ExecuteAsync(CancellationToken.None);


			if (expectedException)
			{
				Assert.Equal(400, (int)response.StatusCode);
				Assert.IsType<BadRequestErrorMessageResult>(actionResult);
				string content = await response.Content.ReadAsStringAsync();
				output.WriteLine(content);
			}
			else
			{
				Assert.Equal(200, (int)response.StatusCode);
				output.WriteLine("Status code: " + (int)response.StatusCode);
			}

		}






		[Theory]
		[MemberData(nameof(MemberControllerTestData.ChangePasswordTestData), MemberType = typeof(MemberControllerTestData))]
		public async Task TestChangePassword_ReturnOk(int id, Argument arg)
		{

			var actionResult = await apiMemberController.ChangePassword(id, arg);
			var response = await actionResult.ExecuteAsync(CancellationToken.None);
			string content = await response.Content.ReadAsStringAsync();


			output.WriteLine(content);

		}









		[Theory]
		[InlineData(true, 0)]
		[InlineData(false, 1)]
		[InlineData(true, int.MaxValue)]
		public async Task TestResignMember_ReturnOk(bool expectedNull, int id)
		{
			var actionResult = await apiMemberController.ResignMember(id);

			if (!expectedNull)
			{
				var ar = await apiMemberController.GetMember(id);
				var res = await ar.ExecuteAsync(CancellationToken.None);
				string cont = await res.Content.ReadAsStringAsync();
				Assert.Equal(false, JsonConvert.DeserializeObject<User>(cont).is_active);
			}
			else
			{
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				string content = await response.Content.ReadAsStringAsync();
				output.WriteLine(content);
			}
		}


		#endregion

	}
















	/*Data to test*/
	public static class MemberControllerTestData
	{
		public static IEnumerable<object[]> GetMemberListTestData()
		{
			yield return new object[] { true, null! };
			yield return new object[] { false, new Argument() { text = "", role = "" } };
			yield return new object[] { false, new Argument() { text = "", role = "SM" } };
			yield return new object[] { false, new Argument() { text = "Ly", role = "" } };
			yield return new object[] { false, new Argument() { text = "Ly", role = "HR" } };
			yield return new object[] { false, new Argument() { text = "A", role = "MB" } };
			yield return new object[] { false, new Argument() { text = "Duc", role = "PM" } };
		}


		public static IEnumerable<object[]> CreateNewMemberTestData()
		{
			//yield return new object[] { true, null! };
			yield return new object[] { true, new UserDTO() { fullname = "" } };

			//yield return new object[] { false, new UserDTO() { fullname = "Trần Đức" } };
			//yield return new object[] { false, new UserDTO() { fullname = "Nguyễn Công Minh" } };
		}



		public static IEnumerable<object[]> UpdateMemberTestData()
		{
			var a = new UserDTO() { user_code = "", username = "", fullname = "" };
			var b = new UserDTO() { user_code = "LuyenLV", username = "LuyenLV", fullname = "Lê Văn Luyện" };
			var c = new UserDTO() { user_code = "LuyenLV", username = "LuyenLV", fullname = "" };
			var d = new UserDTO() { user_code = "LuyenLV", username = "", fullname = "Lê Văn Luyện" };
			var e = new UserDTO() { user_code = "", username = "LuyenLV", fullname = "Lê Văn Luyện" };

			yield return new object[] { true, 21, null! };
			yield return new object[] { false, 21, a };
			yield return new object[] { false, 20, b };
			yield return new object[] { false, 4, c };
			yield return new object[] { false, 15, d };
			yield return new object[] { false, 13, e };
		}





		public static IEnumerable<object[]> ChangePasswordTestData()
		{
			// text is real pass, text 1 is new pass, text 2 is confirm pass
			yield return new object[] { 0, null! };
			yield return new object[] { 1, new Argument() { text = "123", text1 = "hello", text2 = "hello" } };
			yield return new object[] { 1, new Argument() { text = "", text1 = "hello", text2 = "hello" } };

			yield return new object[] { 1, new Argument() { text = "1", text1 = "lo", text2 = "hello" } };
			yield return new object[] { 1, new Argument() { text = "1", text1 = "", text2 = "hello" } };

			yield return new object[] { 1, new Argument() { text = "1", text1 = "hello", text2 = "" } };
			yield return new object[] { 1, new Argument() { text = "1", text1 = "hello", text2 = "hello" } };
		}

	}
}
