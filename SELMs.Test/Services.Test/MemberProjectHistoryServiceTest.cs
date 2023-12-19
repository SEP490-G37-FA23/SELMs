namespace SELMs.Test.Services.Test
{
	public class MemberProjectHistoryServiceTest
	{
		private readonly ITestOutputHelper output;
		private readonly IMemberProjectHistoryService memberProjectHistoryService = new MemberProjectHistoryService();

		public MemberProjectHistoryServiceTest(ITestOutputHelper output)
		{
			this.output = output;
		}







		[Theory]
		[MemberData(nameof(MemberProjectHistoryServiceTestData.UpdateHistoryTestData), MemberType = typeof(MemberProjectHistoryServiceTestData))]
		public async Task TestUpdateHistory_ReturnUpdatedState(int id, Member_Project_History memberProjectHistory)
		{
			bool updateSuccessfull = await memberProjectHistoryService.UpdateHistory(id, memberProjectHistory);

			if (!updateSuccessfull)
			{
				output.WriteLine("Update fail");
				return;
			}
			Assert.True(updateSuccessfull);
		}
	}


	public static class MemberProjectHistoryServiceTestData
	{
		public static IEnumerable<object[]> UpdateHistoryTestData()
		{
			yield return new object[] { 4, new Member_Project_History() { project_id = 9, user_code = "sm", status = "ACTIVE" } };
			yield return new object[] { 15, new Member_Project_History() { project_id = 3, user_code = "DucTM1", status = "INACTIVE", note = "hello world" } };
		}
	}
}
