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
	public class ApiEquipmentProjectHistoryController : ApiController
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(ApiCategoryController));


		private readonly IMapper mapper = MapperConfig.Initialize();
		private readonly IEquipmentProjectHistoryService equipmentProjectHistoryService = new EquipmentProjectHistoryService();
		private readonly IEquipmentProjectHistoryRepository equipmentProjectHistoryRepository = new EquipmentProjectHistoryRepository();





		#region Get list
		[HttpGet]
		[Route("equipment-project-histories")]
		public async Task<IHttpActionResult> GetListEquipmentLocationHistory()
		{
			try
			{
				List<Equipment_Project_History> equipmenthistoryList = await equipmentProjectHistoryRepository.GetEquipmentProjectHistoryList();
				return Ok(equipmenthistoryList);
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










		#region Create
		[HttpPost]
		[Route("equipment-project-history/create")]
		public async Task<IHttpActionResult> AddEquipmentLocationHistory(EquipmentProjectHistoryDTO equipmentProjectHistoryDTO)
		{
			try
			{
				Equipment_Project_History equipmentProjectHistory = mapper.Map<Equipment_Project_History>(equipmentProjectHistoryDTO);
				bool addNewSuccessfull = await equipmentProjectHistoryRepository.Add(equipmentProjectHistory);

				if (addNewSuccessfull)
					return Ok("Thêm mới thành công");

				return BadRequest("Thêm mới thất bại");
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
		[Route("equipment-location-history/{id}")]
		public async Task<IHttpActionResult> GetEquipmentLocationHistoryById(int id)
		{
			try
			{
				var equipmenthistory = await equipmentProjectHistoryRepository.GetEquipmentProjectHistoryById(id);
				return Ok(equipmenthistory ?? null);
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
		[Route("equipment-project-history/{id}")]
		public async Task<IHttpActionResult> UpdateEquipmentLocationHistory(int id, [FromBody] EquipmentProjectHistoryDTO equipmentProjectHistoryDTO)
		{
			try
			{
				Equipment_Project_History equipmentLocationHistory = mapper.Map<Equipment_Project_History>(equipmentProjectHistoryDTO);
				bool updateSuccessfull = await equipmentProjectHistoryService.UpdateEquipmentProjectHistory(id, equipmentLocationHistory);

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
