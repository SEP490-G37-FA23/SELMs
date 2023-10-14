using System;
using System.Threading.Tasks;
using System.Web.Http;
using log4net;
using SELMs.Models.BusinessModel;
using SELMs.Repositories;
using SELMs.Repositories.Implements;
using SELMs.Services;

namespace SELMs.Api.HumanResource
{
	public class MemberController : ApiController
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(MemberController));


		private readonly IMemberService memberService = new MemberService();
		private readonly IMemberRepository memberRepository = new MemberRepository();



		// GET: Api_Member
		#region Danh sách khách hàng
		[HttpPost]
		[Route("api/Api_Member/GetMembersList")]
		public async Task<IHttpActionResult> GetMemberList(Argument args)
		{
			try
			{
				dynamic returnedData = null;
				returnedData = await memberRepository.GetMemberList(args);
				return Ok(returnedData);
			}
			catch (Exception ex)
			{
				Log.Error("Error: ", ex);
				Console.WriteLine($"{ex.Message} \n {ex.StackTrace}");
				return InternalServerError();
				throw;
			}


		}
		#endregion






		[HttpPut]
		[Route("api/Api_Member/UpdateMember")]
		public async Task<IHttpActionResult> UpdateMember([FromBody] dynamic member)
		{
			try
			{
				await memberService.UpdateMember(member);

				return Ok("Member updated successfully");
			}
			catch (Exception ex)
			{
				Log.Error("Error: ", ex);
				Console.WriteLine($"{ex.Message} \n {ex.StackTrace}");
				return InternalServerError();
				throw;
			}
		}





		[HttpPut]
		[Route("api/Api_Member/MarkMemberQuit")]
		public async Task<IHttpActionResult> MarkMemberQuit(string memberCodeOrUsername)
		{
			try
			{
				dynamic memberFound = await memberRepository.GetMemberByCodeOrUsername(memberCodeOrUsername);

				if (memberFound == null)
					return NotFound();

				await memberService.MarkMemberQuit(memberFound);

				return Ok("Mark member quit successfully");
			}
			catch (Exception ex)
			{
				Log.Error("Error: ", ex);
				Console.WriteLine($"{ex.Message} \n {ex.StackTrace}");
				return InternalServerError();
				throw;
			}
		}
	}
}