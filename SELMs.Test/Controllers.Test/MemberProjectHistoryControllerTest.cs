using System.Web.Http.Results;

namespace SELMs.Test.Controllers.Test
{
	public class MemberProjectHistoryControllerTest
	{
		private readonly ITestOutputHelper output;
		private readonly ApiMemberProjectHistoryController apiMemberProjectHistoryController = new();


		public MemberProjectHistoryControllerTest(ITestOutputHelper output)
		{
			this.output = output;
			ApiControllerSetup.SetupController(apiMemberProjectHistoryController);
		}



		[Fact]
		public async Task TestGetListHistory_ReturnHistoryList()
		{
			try
			{
				var actionResult = await apiMemberProjectHistoryController.GetList();
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				string content = await response.Content.ReadAsStringAsync();

				output.WriteLine($"Test case passed - Status code: {(int)response.StatusCode}\n{content}");
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
				var actionResult = await apiMemberProjectHistoryController.GetHistoryById(id);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				string content = await response.Content.ReadAsStringAsync();


				if (content.Equals("null"))
					output.WriteLine($"Test case passed - Status code: {(int)response.StatusCode}\nHistory not found");

				else
					output.WriteLine($"Test case passed - Status code: {(int)response.StatusCode}\n{content}");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}







		[Theory]
		[MemberData(nameof(MemberProjectHistoryControllerTestData.SaveHistoryTestData), MemberType = typeof(MemberProjectHistoryControllerTestData))]
		public async Task TestSaveHistory_ReturnNoException(MemberProjectHistoryDTO memberProjectHistoryDTO)
		{
			try
			{
				var actionResult = await apiMemberProjectHistoryController.SaveHistory(memberProjectHistoryDTO);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				string content = await response.Content.ReadAsStringAsync();

				Assert.Equal("\"Thêm mới thành công\"", content.Trim());
				output.WriteLine($"Test case passed - Status code: {(int)response.StatusCode}\n{content}");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}





		[Theory]
		[MemberData(nameof(MemberProjectHistoryControllerTestData.UpdateHistoryTestData), MemberType = typeof(MemberProjectHistoryControllerTestData))]
		public async Task TestUpdateHistory_ReturnNoException(bool expectError, int id, MemberProjectHistoryDTO memberProjectHistoryDTO)
		{
			try
			{
				var actionResult = await apiMemberProjectHistoryController.UpdateHistory(id, memberProjectHistoryDTO);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);


				string? message = "";
				if (actionResult is OkNegotiatedContentResult<string> || actionResult is BadRequestErrorMessageResult)
				{
					if (!expectError)
						Assert.Equal("Cập nhật thành công", message = (actionResult as OkNegotiatedContentResult<string>)?.Content);
					else
						Assert.Equal("Cập nhật thất bại", message = (actionResult as BadRequestErrorMessageResult)?.Message);
				}
				output.WriteLine($"Test case passed - Status code: {(int)response.StatusCode}\n{message}");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}
	}









	public static class MemberProjectHistoryControllerTestData
	{
		public static IEnumerable<object[]> SaveHistoryTestData()
		{
			yield return new object[] { new MemberProjectHistoryDTO() { project_id = 2, user_code = "DatTT", status = "ACTIVE", date = DateTime.Now } };
			yield return new object[] { new MemberProjectHistoryDTO() { project_id = 4, user_code = "LuyenLV", status = "INACTIVE", date = DateTime.Now } };
			yield return new object[] { new MemberProjectHistoryDTO() { project_id = 0, user_code = "LuatNV", status = "INACTIVE", date = DateTime.Now } };
		}



		public static IEnumerable<object[]> UpdateHistoryTestData()
		{
			var a = new MemberProjectHistoryDTO() { project_id = 3, user_code = "DucTM1", status = "INACTIVE", note = "huh?" };
			var b = new MemberProjectHistoryDTO() { project_id = -7, user_code = "", status = "ACTIVE" };
			var c = new MemberProjectHistoryDTO() { project_id = 2, user_code = "DatTT", status = "" };
			var d = new MemberProjectHistoryDTO() { project_id = 9, user_code = "sm", status = "ACTIVE", note = "ok yeah" };

			yield return new object[] { false, 1, a };
			yield return new object[] { true, 0, b };
			yield return new object[] { true, -2, c };
			yield return new object[] { false, 4, d };
		}
	}
}
