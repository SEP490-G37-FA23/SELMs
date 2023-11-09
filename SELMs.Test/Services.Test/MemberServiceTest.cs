using SELMs.Services.Implements;
using Xunit.Abstractions;

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
		public async Task TestUpdateMember_ReturnNothing(int id, User userDTO)
		{
			try
			{
				await memberService.UpdateMember(id, userDTO);
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
			yield return new object[] { new User() { fullname = "quốc" } };
			yield return new object[] { new User() { fullname = "Hồ Chí Minh" } };
			yield return new object[] { new User() { fullname = "Trịnh Văn Quyết" } };
			yield return new object[] { new User() { fullname = "Vương Đình Huệ" } };
			yield return new object[] { new User() { fullname = "Phạm Minh Chính" } };
			yield return new object[] { new User() { fullname = "Nguyễn Phú Trọng" } };
		}


		public static IEnumerable<object[]> UpdateMemberTestData()
		{
			yield return new object[] { 1, new User() { fullname = "Hà" } };
			yield return new object[] { 3, new User() { fullname = "Nguyễn Tuấn Anh" } };
			yield return new object[] { 4, new User() { fullname = "Lê Văn Luyện" } };
		}



	}
}
