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

			var a = await memberController.GetMemberList(null);

			output.WriteLine(a.ToString());
			Assert.NotNull(a);
		}
	}
}