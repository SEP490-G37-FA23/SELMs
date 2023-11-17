namespace SELMs.Test.Services.Test
{
	public class MemberServiceTest
	{

		private readonly ITestOutputHelper output;
		private readonly IMemberService memberService = new MemberService();


		public MemberServiceTest(ITestOutputHelper output)
		{
			this.output = output;
		}



		#region Iteration 1

		[Theory]
		[MemberData(nameof(MemberServiceTestData.SaveMemberTestData), MemberType = typeof(MemberServiceTestData))]
		public async Task TestSaveMember_ReturnNothing(User userDTO)
		{
			try
			{
				await memberService.SaveMember(userDTO);
				output.WriteLine("Test case passed");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}




		[Theory]
		[MemberData(nameof(MemberServiceTestData.UpdateMemberTestData), MemberType = typeof(MemberServiceTestData))]
		public async Task TestUpdateMember_ReturnNothing(int id, User user)
		{
			try
			{
				await memberService.UpdateMember(id, user);
				output.WriteLine("Test case passed");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}


		#endregion
	}








	/*Data to test*/
	public static class MemberServiceTestData
	{
		public static IEnumerable<object[]> SaveMemberTestData()
		{
			yield return new object[] { new User() { fullname = "Phan Văn Giang" } };
			yield return new object[] { new User() { fullname = "Vương Đình Huệ" } };
			yield return new object[] { new User() { fullname = "Nguyễn Chí Vịnh" } };
		}


		public static IEnumerable<object[]> UpdateMemberTestData()
		{
			yield return new object[] { 0, new User() { user_code = "TanLT", username = "TanLT", fullname = "Lê Trọng Tấn" } };
			yield return new object[] { 4, new User() { user_code = "QuyenNV", username = "QuyenNV", fullname = "Vũ Văn Quyền" } };
			yield return new object[] { 5, new User() { user_code = "TuNT", username = "TuNT", fullname = "Nguyễn Tuấn Tú" } };
		}



	}
}
