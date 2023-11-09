using Newtonsoft.Json;
using SELMs.Repositories;
using SELMs.Repositories.Implements;
using Xunit.Abstractions;

namespace SELMs.Test.Repositories.Test
{
	public class MemberRepositoryTest
	{

		private readonly ITestOutputHelper output;
		private readonly IMemberRepository memberRepository = new MemberRepository();

		public MemberRepositoryTest(ITestOutputHelper output)
		{

			this.output = output;
		}





		#region Iteration 1


		[Theory]
		[MemberData(nameof(MemberRepositoryTestData.GetMemberListTestData), MemberType = typeof(MemberRepositoryTestData))]
		public async Task TestGetMemberList_ReturnMemberList(Argument argument)
		{
			try
			{
				var list = await memberRepository.GetMemberList(argument);
				output.WriteLine("Test case passed");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}





		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(3)]
		[InlineData(66)]
		public async Task TestGetMember_ReturnMemberById(int id)
		{
			try
			{
				User? member = await memberRepository.GetMember(id);

				if (member == null)
					output.WriteLine("Member not found");
				else
					output.WriteLine("Member found:\n" + JsonConvert.SerializeObject(member));
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}




		[Theory]
		[MemberData(nameof(MemberRepositoryTestData.CreateNewMemberTestData), MemberType = typeof(MemberRepositoryTestData))]
		public async Task TestSaveMember_ReturnNothing(User user)
		{
			try
			{
				await memberRepository.SaveMember(user);
				output.WriteLine("Test case passed");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}




		[Theory]
		[MemberData(nameof(MemberRepositoryTestData.UpdateMemberTestData), MemberType = typeof(MemberRepositoryTestData))]
		public void TestUpdateMember_ReturnNothing(User user)
		{
			try
			{
				memberRepository.UpdateMember(user);
				output.WriteLine("Test case passed");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}
		#endregion





	}












	public static class MemberRepositoryTestData
	{
		public static IEnumerable<object[]> GetMemberListTestData()
		{
			yield return new object[] { new Argument() { username = "DatTT", isadmin = false, role = "SM" } };
			yield return new object[] { new Argument() { fullname = "da" } };
			yield return new object[] { new Argument() { fullname = "Ly" } };
		}



		public static IEnumerable<object[]> CreateNewMemberTestData()
		{
			yield return new object[] { new User() { fullname = "" } };
			yield return new object[] { new User() { fullname = "Phạm Văn Đồng" } };
			yield return new object[] { new User() { fullname = "Trần Đình Trọng" } };
		}



		public static IEnumerable<object[]> UpdateMemberTestData()
		{
			yield return new object[] { new User() { user_id = 12, fullname = "" } };
			yield return new object[] { new User() { user_id = 7, fullname = "Hà" } };
			yield return new object[] { new User() { user_id = 4, fullname = "Nguyễn Tuấn Anh" } };
		}


	}
}
