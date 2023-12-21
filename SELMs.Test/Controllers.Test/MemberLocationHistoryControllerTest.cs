namespace SELMs.Test.Controllers.Test
{
	public class MemberLocationHistoryControllerTest
	{
		private readonly ITestOutputHelper output;
		private readonly ApiMemberLocationHistoryController apiMemberLocationHistoryController = new();

		public MemberLocationHistoryControllerTest(ITestOutputHelper output)
		{
			this.output = output;
			ApiControllerSetup.SetupController(apiMemberLocationHistoryController);
		}




		#region Iteration 3

		[Fact]
		public async Task TestGetMemberLocationHistoryList_ReturnList()
		{
			try
			{
				var actionResult = await apiMemberLocationHistoryController.GetMemberLocationHistoryList();
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				string content = await response.Content.ReadAsStringAsync();

				output.WriteLine(content);
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}




		[Theory]
		[InlineData(0)]
		[InlineData(2)]
		public async Task TestGetMemberLocationHistoryById_ReturnHistory(int id)
		{
			try
			{
				var actionResult = await apiMemberLocationHistoryController.GetMemberLocationHistoryById(id);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				string content = await response.Content.ReadAsStringAsync();

				if (content.Equals("null"))
					output.WriteLine("History not found");
				else
					output.WriteLine(content);
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}




		[Theory]
		[MemberData(nameof(MemberLocationHistoryControllerTestData.CreateHistoryTestData), MemberType = typeof(MemberLocationHistoryControllerTestData))]
		public async Task TestCreateHistory_ReturnHistory(MemberLocationHistoryDTO memberLocationHistoryDTO)
		{
			try
			{
				var actionResult = await apiMemberLocationHistoryController.SaveMemberLocationHistory(null);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				string content = await response.Content.ReadAsStringAsync();

				output.WriteLine(content);
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}




		[Theory]
		[MemberData(nameof(MemberLocationHistoryControllerTestData.UpdateHistoryTestData), MemberType = typeof(MemberLocationHistoryControllerTestData))]
		public async Task TestUpdateHistory_ReturnSuccessOrFailMessage(int id, MemberLocationHistoryDTO memberLocationHistoryDTO)
		{
			try
			{
				var actionResult = await apiMemberLocationHistoryController.UpdateMemberLocationHistory(id, memberLocationHistoryDTO);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				string content = await response.Content.ReadAsStringAsync();

				output.WriteLine(content);
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}
		#endregion
	}








	internal static class MemberLocationHistoryControllerTestData
	{
		public static IEnumerable<object[]> CreateHistoryTestData()
		{
			yield return new object[] { new MemberLocationHistoryDTO() { location_id = 3, reason = "testing" } };
			yield return new object[] { new MemberLocationHistoryDTO() { location_id = 5, reason = "out of date" } };
		}


		public static IEnumerable<object[]> UpdateHistoryTestData()
		{
			yield return new object[] { 0, new MemberLocationHistoryDTO() { location_id = 3, reason = "testing" } };
			yield return new object[] { 2, new MemberLocationHistoryDTO() { location_id = 10, reason = "dunno what to do" } };
		}
	}
}