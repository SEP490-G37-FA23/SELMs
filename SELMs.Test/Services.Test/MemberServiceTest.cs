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
			await memberService.SaveMember(userDTO);
			output.WriteLine("Test case passed");
		}




		[Theory]
		[MemberData(nameof(MemberServiceTestData.UpdateMemberTestData), MemberType = typeof(MemberServiceTestData))]
		public async Task TestUpdateMember_ReturnNoException(int id, User user)
		{
			await memberService.UpdateMember(id, user);
			output.WriteLine("Test case passed");
		}


		#endregion
	}








	/*Data to test*/
	public static class MemberServiceTestData
	{
		public static IEnumerable<object[]> SaveMemberTestData()
		{
			yield return new object[] { new User() { fullname = "Phan Văn Giang" } };
			yield return new object[] { new User() { } };
			yield return new object[] { new User() { fullname = "" } };
		}


		public static IEnumerable<object[]> UpdateMemberTestData()
		{
			yield return new object[] { 0, new User() { user_code = "", username = "", fullname = "" } };
			yield return new object[] { 4, new User() { user_code = "QuyenNV", username = "QuyenNV", fullname = "" } };
			yield return new object[] { 5, new User() { user_code = "TuNT", username = "", fullname = "Nguyễn Tuấn Tú" } };
			yield return new object[] { 7, new User() { user_code = "CuongNV", username = "CuongNV", fullname = "Nguyễn Văn Cường" } };
		}



	}
}
