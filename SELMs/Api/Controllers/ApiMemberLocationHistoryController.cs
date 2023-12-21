using System;
using System.Collections.Generic;
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
	public class ApiMemberLocationHistoryController : ApiController
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(ApiMemberLocationHistoryController));

		private readonly IMapper mapper = MapperConfig.Initialize();
		private readonly IMemberLocationHistoryService memberLocationHistoryService = new MemberLocationHistoryService();
		private readonly IMemberLocationHistoryRepository memberLocationHistoryRepository = new MemberLocationHistoryRepository();


		#region Get list
		[HttpGet]
		[Route("member-location-histories")]
		public async Task<IHttpActionResult> GetMemberLocationHistoryList()
		{
			try
			{
				var list = await memberLocationHistoryRepository.GetMemberLocationHistoryList();
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



		#region Get by id
		[HttpGet]
		[Route("member-location-histories/{id}")]
		public async Task<IHttpActionResult> GetMemberLocationHistoryById(int id)
		{
			try
			{
				var historyFound = await memberLocationHistoryRepository.GetMemberLocationHistoryById(id);

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
		[Route("member-location-history/create")]
		public async Task<IHttpActionResult> SaveMemberLocationHistory([FromBody] ListMemberLocationHistoryDTO dto)
		{
			try
			{
				var histories = mapper.Map<List<Member_Location_History>>(dto.ListMembersJoinLocation);
				List<dynamic> result = new List<dynamic>();
				foreach (var history in histories)
				{
					history.date = DateTime.Now;
					var item = memberLocationHistoryRepository.AddMemberLocationHistory(history);
					result.Add(history);
				}
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




		#region Update 
		[HttpPut]
		[Route("member-location-histories/{id}")]
		public async Task<IHttpActionResult> UpdateMemberLocationHistory(int id, [FromBody] MemberLocationHistoryDTO memberLocationHistoryDTO)
		{
			try
			{
				var history = mapper.Map<Member_Location_History>(memberLocationHistoryDTO);
				bool updateSuccessfull = await memberLocationHistoryService.UpdateHistory(id, history);

				if (updateSuccessfull)
					return Ok("Cập nhật thành công");

				return BadRequest("Cập nhật thất bại");
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
