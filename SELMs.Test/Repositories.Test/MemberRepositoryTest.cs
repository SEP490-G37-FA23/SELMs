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

				Assert.IsType<List<dynamic>>(list);

				if (list is List<dynamic> { Count: > 0 })
				{
					foreach (var item in list)
						output.WriteLine(JsonConvert.SerializeObject(item));
				}
				else
					output.WriteLine("No user found");
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
		public async Task TestGetMember_ReturnMemberById(int id)
		{

			User? member = await memberRepository.GetMember(id);

			if (member == null)
				output.WriteLine("Member not found");
			else
				output.WriteLine("Member found:\n" + JsonConvert.SerializeObject(member));

		}




		[Theory]
		[MemberData(nameof(MemberRepositoryTestData.CreateNewMemberTestData), MemberType = typeof(MemberRepositoryTestData))]
		public void TestSaveMember_ReturnNoException(User user)
		{

			memberRepository.SaveMember(user);
			Thread.Sleep(2000);
			output.WriteLine("Test case passed");

		}




		[Theory]
		[MemberData(nameof(MemberRepositoryTestData.UpdateMemberTestData), MemberType = typeof(MemberRepositoryTestData))]
		public void TestUpdateMember_ReturnNoException(User user)
		{
			try
			{

			}
			catch (Exception)
			{

			}


			memberRepository.UpdateMember(user);
			Thread.Sleep(2000);
			output.WriteLine("Test case passed");

		}
		#endregion





	}












	public static class MemberRepositoryTestData
	{
		public static IEnumerable<object[]> GetMemberListTestData()
		{
			yield return new object[] { new Argument() { text = string.Empty } };
			yield return new object[] { new Argument() { text = "Ly" } };
			yield return new object[] { new Argument() { text = "Duc" } };
			yield return new object[] { new Argument() { text = "cường" } };
		}



		public static IEnumerable<object[]> CreateNewMemberTestData()
		{
			yield return new object[] { new User() { username = "", user_code = "", fullname = "", password = "" } };
			yield return new object[] { new User() { username = "DongPV", user_code = "DongPV", fullname = "Phạm Văn Đồng", password = "123" } };
			yield return new object[] { new User() { username = "TrongTD", user_code = "TrongTD", fullname = "Trần Đình Trọng", password = "123" } };
		}



		public static IEnumerable<object[]> UpdateMemberTestData()
		{
			yield return new object[] { new User() { user_id = 17, username = "", user_code = "", fullname = "", password = "" } };
			//yield return new object[] { new User() { user_id = 20, username = "QuangTX", user_code = "QuangTX", fullname = "Trần Xuân Quang", password = "hello there" } };
			//yield return new object[] { new User() { user_id = 4, username = "QuangTD", user_code = "QuangTD", fullname = "Trần Đại Quang", password = "emyeu123" } };
		}


	}
}
