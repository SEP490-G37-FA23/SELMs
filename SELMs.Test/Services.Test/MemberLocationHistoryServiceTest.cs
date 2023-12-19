namespace SELMs.Test.Services.Test
{
	public class MemberLocationHistoryServiceTest
	{
		private readonly ITestOutputHelper output;
		private readonly IMemberLocationHistoryService memberLocationHistoryService = new MemberLocationHistoryService();


		public MemberLocationHistoryServiceTest(ITestOutputHelper output)
		{
			this.output = output;
		}


		[Theory]
		[MemberData(nameof(MemberLocationHistoryServiceTestData.UpdateHistoryTestData), MemberType = typeof(MemberLocationHistoryServiceTestData))]
		public async Task TestUpdateHistory_ReturnNoException(int id, Member_Location_History memberLocationHistory)
		{
			try
			{
				bool updateSuccess = await memberLocationHistoryService.UpdateHistory(id, memberLocationHistory);

				if (updateSuccess)
					output.WriteLine("Test case passed");
				else
					output.WriteLine("Test case fail");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}

	}


	internal static class MemberLocationHistoryServiceTestData
	{
		public static IEnumerable<object[]> UpdateHistoryTestData()
		{
			yield return new object[] { 0, new Member_Location_History() { location_id = 1, reason = "yolo" } };
			yield return new object[] { 2, new Member_Location_History() { location_id = 3, reason = "hello world" } };
		}
	}
}
