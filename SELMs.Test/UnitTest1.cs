using System.Web.Http.Results;
using SELMs.Api.HumanResource;
using Xunit.Abstractions;

namespace SELMs.Test
{
	public class UnitTest1
	{

		ITestOutputHelper output;

		public UnitTest1(ITestOutputHelper output)
		{
			this.output = output;
		}

		[Fact]
		public async Task Test1()
		{
			ApiMemberController memberController = new();
			//OkNegotiatedContentResult<dynamic>;
			var a = await memberController.GetMemberList(null);

			output.WriteLine(a.ToString());
			Assert.IsType<OkNegotiatedContentResult<dynamic>>(a);
		}
	}
}