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
		[MemberData(nameof(MemberControllerTestData.UpdateMemberTestData), MemberType = typeof(MemberControllerTestData))]
		public async Task TestUpdateMember_ReturnOk(int id, UserDTO userDTO)
		{
			try
			{
				var actionResult = await apiMemberController.UpdateMember(id, userDTO);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);

				output.WriteLine($"Update successfull - Status code: {(int)response.StatusCode}");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
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
			yield return new object[] { new Argument() { text = "" } };
			yield return new object[] { new Argument() { text = "Anh" } };
			yield return new object[] { new Argument() { text = "Ly" } };
		}


		public static IEnumerable<object[]> CreateNewMemberTestData()
		{
			yield return new object[] { new UserDTO() { fullname = "Phạm Minh Chính" } };
			yield return new object[] { new UserDTO() { fullname = "Hồ Chí Minh" } };
			yield return new object[] { new UserDTO() { fullname = "Nguyễn Phú Trọng" } };
		}



		public static IEnumerable<object[]> UpdateMemberTestData()
		{
			yield return new object[] { 0, new UserDTO() { user_code = "CuongHA", username = "123", fullname = "Hoàng Anh Cường" } };
			yield return new object[] { 6, new UserDTO() { user_code = "AnhTN", username = "AnhTN", fullname = "Nguyễn Tuấn Anh" } };
			yield return new object[] { 7, new UserDTO() { user_code = "LuyenLV", username = "LuyenLV", fullname = "Lê Văn Luyện" } };
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
