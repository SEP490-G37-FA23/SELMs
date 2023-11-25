using System;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using log4net;
using SELMs.Api.DTOs;
using SELMs.App_Start;
using SELMs.Models;
using SELMs.Repositories;
using SELMs.Repositories.Implements;
using SELMs.Services;
using SELMs.Services.Implements;

namespace SELMs.Api.Controllers
{
	[RoutePrefix("api/v1")]
	public class ApiMemberProjectHistoryController : ApiController
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(ApiMemberProjectHistoryController));

		private readonly IMapper mapper = MapperConfig.Initialize();
		private IMemberProjectHistoryService memberProjectHistoryService = new MemberProjectHistoryService();
		private readonly IMemberProjectHistoryRepository memberProjectHistoryRepository = new MemberProjectHistoryRepository();


		#region Get list
		[HttpGet]
		[Route("member-project-histories")]
		public async Task<IHttpActionResult> GetList()
		{
			try
			{
				var list = await memberProjectHistoryRepository.GetMemberProjectHistoryList();
				return Ok(list);
			}
			catch (Exception ex)
			{
				Log.Error("Error: ", ex);
				Console.WriteLine($"{ex.Message} \n {ex.StackTrace}");
				return BadRequest($"{ex.Message} \n {ex.StackTrace}");
				throw;
			}
		}
		#endregion


		#region Get id
		[HttpGet]
		[Route("member-project-histories/{id}")]
		public async Task<IHttpActionResult> GetHistoryById(int id)
		{
			try
			{
				var historyFound = await memberProjectHistoryRepository.GetMemberProjectHistoryById(id);
				return Ok(historyFound ?? null);
			}
			catch (Exception ex)
			{
				Log.Error("Error: ", ex);
				Console.WriteLine($"{ex.Message} \n {ex.StackTrace}");
				return BadRequest($"{ex.Message} \n {ex.StackTrace}");
				throw;
			}
		}
		#endregion

		#region Add new
		[HttpPost]
		[Route("member-project-histories/create")]
		public async Task<IHttpActionResult> SaveCategory([FromBody] MemberProjectHistoryDTO memberProjectHistoryDTO)
		{
			try
			{
				var newHistory = mapper.Map<Member_Project_History>(memberProjectHistoryDTO);
				await memberProjectHistoryRepository.SaveHistory(newHistory);
				return Ok("Thêm mới thành công");
			}
			catch (Exception ex)
			{
				Log.Error("Error: ", ex);
				Console.WriteLine($"{ex.Message} \n {ex.StackTrace}");
				return BadRequest($"{ex.Message} \n {ex.StackTrace}");
				throw;
			}
		}
		#endregion

	}
}
