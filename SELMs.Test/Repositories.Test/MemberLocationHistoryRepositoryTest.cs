namespace SELMs.Test.Repositories.Test
{
	public class MemberLocationHistoryRepositoryTest
	{
		private readonly ITestOutputHelper output;
		private readonly IMemberLocationHistoryRepository memberLocationHistoryRepository = new MemberLocationHistoryRepository();



		public MemberLocationHistoryRepositoryTest(ITestOutputHelper output)
		{
			this.output = output;
		}



		[Fact]
		public async Task TestGetMemberLocationHistoryList_ReturnList()
		{
			var list = await memberLocationHistoryRepository.GetMemberLocationHistoryList();

			foreach (var item in list)
				output.WriteLine(JsonConvert.SerializeObject(item));
		}



		[Theory]
		[InlineData(0)]
		[InlineData(2)]
		public async Task TestGetMemberLocationHistoryById_ReturnHistory(int id)
		{
			Member_Location_History history = await memberLocationHistoryRepository.GetMemberLocationHistoryById(id);

			if (history != null)
				output.WriteLine(JsonConvert.SerializeObject(history));
			else
				output.WriteLine("History not found");
		}





		[Theory]
		[MemberData(nameof(MemberLocationHistoryRepositoryTestData.CreateHistoryTestData), MemberType = typeof(MemberLocationHistoryRepositoryTestData))]
		public async Task TestCreateMemberLocationHistory_ReturnCreatedStatus(Member_Location_History history)
		{
			try
			{
				bool createdSuccesfull = await memberLocationHistoryRepository.AddMemberLocationHistory(history);

				if (createdSuccesfull)
					output.WriteLine("Create successfull");
				else
					output.WriteLine("Create fail");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}


		[Theory]
		[MemberData(nameof(MemberLocationHistoryRepositoryTestData.UpdateHistoryTestData), MemberType = typeof(MemberLocationHistoryRepositoryTestData))]
		public async Task TestUpdateMemberLocationHistory_ReturnUpdateStatus(Member_Location_History history)
		{
			try
			{
				bool updateSuccesfull = await memberLocationHistoryRepository.UpdateMemberLocationHistory(history);

				if (updateSuccesfull)
					output.WriteLine("Update successfull");
				else
					output.WriteLine("Update fail");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}
	}



	internal static class MemberLocationHistoryRepositoryTestData
	{
		public static IEnumerable<object[]> CreateHistoryTestData()
		{
			yield return new object[] { new Member_Location_History() { location_id = 3, reason = "yoda" } };
			yield return new object[] { new Member_Location_History() { location_id = 5, reason = "Darth Vader" } };
		}


		public static IEnumerable<object[]> UpdateHistoryTestData()
		{
			yield return new object[] { new Member_Location_History() { id = 0, location_id = 3, reason = "testing" } };
			yield return new object[] { new Member_Location_History() { id = 3, location_id = 10, reason = "someone took it" } };
		}
	}
}
