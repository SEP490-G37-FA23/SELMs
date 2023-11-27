using System.Collections;

namespace SELMs.Test.Repositories.Test
{
	public class MemberProjectHistoryRepositoryTest
	{

		private readonly ITestOutputHelper output;
		private readonly IMemberProjectHistoryRepository memberProjectHistoryRepository = new MemberProjectHistoryRepository();


		public MemberProjectHistoryRepositoryTest(ITestOutputHelper output)
		{
			this.output = output;
		}

		[Fact]
		public async Task TestGetListHistory_ReturnHistoryList()
		{
			try
			{
				var list = await memberProjectHistoryRepository.GetMemberProjectHistoryList();

				if ((list as IList).Count > 0)
				{
					foreach (var item in list)
						output.WriteLine(JsonConvert.SerializeObject(item));
				}
				else
					output.WriteLine("History not found");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}



		[Theory]
		[InlineData(0)]
		[InlineData(2)]
		[InlineData(3)]
		public async Task TestGetHistoryById_ReturnHistory(int id)
		{
			try
			{
				var history = await memberProjectHistoryRepository.GetMemberProjectHistoryById(id);

				if (history != null)
					output.WriteLine(JsonConvert.SerializeObject(history));
				else
					output.WriteLine("History not found");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}



		[Theory]
		[MemberData(nameof(MemberProjectHistoryRepositoryTestData.SaveHistoryTestData), MemberType = typeof(MemberProjectHistoryRepositoryTestData))]
		public async Task TestSaveHistory_ReturnNoException(Member_Project_History memberProjectHistory)
		{
			try
			{
				await memberProjectHistoryRepository.SaveHistory(memberProjectHistory);
				output.WriteLine("Add new successfully");

			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}




		[Theory]
		[MemberData(nameof(MemberProjectHistoryRepositoryTestData.UpdateHistoryTestData), MemberType = typeof(MemberProjectHistoryRepositoryTestData))]
		public async Task TestUpdateHistory_ReturnNoException(Member_Project_History memberProjectHistory)
		{
			try
			{
				bool updateSuccessfull = await memberProjectHistoryRepository.UpdateHistory(memberProjectHistory);

				if (updateSuccessfull)
					output.WriteLine("Update successfully");
				else
					output.WriteLine("Update Fail");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}
	}






	internal static class MemberProjectHistoryRepositoryTestData
	{
		public static IEnumerable<object[]> SaveHistoryTestData()
		{
			yield return new object[] { new Member_Project_History() { project_id = 6, user_code = "DatTT", status = "ACTIVE", date = DateTime.Now } };
			yield return new object[] { new Member_Project_History() { project_id = 2, user_code = "LuyenLV", status = "INACTIVE", date = DateTime.Now } };
			yield return new object[] { new Member_Project_History() { project_id = 11, user_code = "LuatNV", status = "INACTIVE", date = DateTime.Now } };
		}



		public static IEnumerable<object[]> UpdateHistoryTestData()
		{
			var a = new Member_Project_History() { id = 19, project_id = 10, user_code = "DucTM1", status = "INACTIVE", note = "huh?" };
			var b = new Member_Project_History() { id = 20, project_id = 20, user_code = "", status = "ACTIVE" };
			var c = new Member_Project_History() { id = 0, project_id = 21, user_code = "DatTT", status = "" };

			yield return new object[] { a };
			yield return new object[] { b };
			yield return new object[] { c };
		}
	}
}
