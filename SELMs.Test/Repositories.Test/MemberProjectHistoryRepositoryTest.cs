using System.Collections;
using System.Data.Entity;

namespace SELMs.Test.Repositories.Test
{
	public class MemberProjectHistoryRepositoryTest
	{
		private readonly ITestOutputHelper output;
		private readonly SELMsContext sELMsContext = new();
		private readonly IMemberProjectHistoryRepository memberProjectHistoryRepository = new MemberProjectHistoryRepository();


		public MemberProjectHistoryRepositoryTest(ITestOutputHelper output)
		{
			this.output = output;
		}

		[Fact]
		public async Task TestGetListHistory_ReturnHistoryList()
		{
			var list = await memberProjectHistoryRepository.GetMemberProjectHistoryList();

			if ((list as IList).Count > 0)
				foreach (var item in list)
					output.WriteLine(JsonConvert.SerializeObject(item));
			else
				output.WriteLine("History not found");
		}



		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		public async Task TestGetHistoryById_ReturnHistory(int id)
		{

			var history = await memberProjectHistoryRepository.GetMemberProjectHistoryById(id);

			if (history != null)
				output.WriteLine(JsonConvert.SerializeObject(history));
			else
				output.WriteLine("History not found");

		}



		[Theory]
		[MemberData(nameof(MemberProjectHistoryRepositoryTestData.SaveHistoryTestData), MemberType = typeof(MemberProjectHistoryRepositoryTestData))]
		public async Task TestSaveHistory_ReturnNoException(Member_Project_History memberProjectHistory)
		{

			await memberProjectHistoryRepository.SaveHistory(memberProjectHistory);

			var project = await sELMsContext.Projects.FirstOrDefaultAsync(p => p.project_id == memberProjectHistory.project_id);

			if (project != null)
			{
				var list = sELMsContext.Member_Project_History.Where(m => m.project_id == project.project_id).ToList();

				foreach (var item in list)
					output.WriteLine(JsonConvert.SerializeObject(item));
			}
		}




		[Theory]
		[MemberData(nameof(MemberProjectHistoryRepositoryTestData.UpdateHistoryTestData), MemberType = typeof(MemberProjectHistoryRepositoryTestData))]
		public async Task TestUpdateHistory_ReturnNoException(string expectedUserCode, Member_Project_History memberProjectHistory)
		{
			try
			{
				bool updateSuccessfull = await memberProjectHistoryRepository.UpdateHistory(memberProjectHistory);

				if (updateSuccessfull)
				{
					var history = await sELMsContext.Member_Project_History.FirstOrDefaultAsync(a => a.id == memberProjectHistory.id);

					Assert.Equal(expectedUserCode, history.user_code);

					output.WriteLine("Update successfully");
				}
				else
					Assert.Fail("Update fail");
			}
			catch (Exception ex)
			{
				Assert.IsType<ArgumentNullException>(ex);
				output.WriteLine(ex.Message);
			}

		}
	}






	internal static class MemberProjectHistoryRepositoryTestData
	{
		public static IEnumerable<object[]> SaveHistoryTestData()
		{
			yield return new object[] { new Member_Project_History() { project_id = 0, user_code = "LuyenLV", status = "INACTIVE", date = DateTime.Now } };
			yield return new object[] { new Member_Project_History() { project_id = 1, user_code = "", status = "", date = null } };
			yield return new object[] { new Member_Project_History() { project_id = 2, user_code = "DatTT", status = "ACTIVE", date = DateTime.Now } };
		}



		public static IEnumerable<object[]> UpdateHistoryTestData()
		{
			var a = new Member_Project_History() { id = 1, project_id = 0, user_code = "DucTM1" };
			var b = new Member_Project_History() { id = 2, project_id = 1, user_code = "" };
			var c = new Member_Project_History() { id = 3, project_id = 3, user_code = "DatTT" };

			yield return new object[] { "DucTM1", a };
			yield return new object[] { "", b };
			yield return new object[] { "DatTT", c };
		}
	}
}
