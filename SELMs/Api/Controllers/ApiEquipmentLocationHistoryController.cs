using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using log4net;
using SELMs.Api.DTOs;
using SELMs.App_Start;
using SELMs.Models;
using SELMs.Repositories.Implements;
using SELMs.Services;
using SELMs.Services.Implements;

namespace SELMs.Api.Controllers
{
	[RoutePrefix("api/v1")]
	public class ApiEquipmentLocationHistoryController : ApiController
	{

		private readonly IMapper mapper = MapperConfig.Initialize();
		private static readonly ILog Log = LogManager.GetLogger(typeof(ApiEquipmentLocationHistoryController));

		private readonly IEquipmentLocationHistoryService equipmentLocationHistoryService = new EquipmentLocationHistoryService();
		private readonly IEquipmentLocationHistoryRepository equipmentLocationHistoryRepository = new EquipmentLocationHistoryRepository();



		#region Get list
		[HttpGet]
		[Route("equipment-location-histories")]
		public async Task<IHttpActionResult> GetListEquipmentLocationHistory()
		{
			try
			{
				List<Equipment_Location_History> equipmenthistoryList = await equipmentLocationHistoryRepository.GetEquipmentLocationHistoryList();
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
		[Route("equipment-location-history/create")]
		public async Task<IHttpActionResult> AddEquipmentLocationHistory(EquipmentLocationHistoryDTO equipmentLocationHistoryDTO)
		{
			try
			{
				Equipment_Location_History equipmentLocationHistory = mapper.Map<Equipment_Location_History>(equipmentLocationHistoryDTO);
				bool addNewSuccessfull = await equipmentLocationHistoryRepository.Add(equipmentLocationHistory);

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
				var equipmenthistory = await equipmentLocationHistoryRepository.GetEquipmentLocationHistoryById(id);
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
		[Route("equipment-location-history/{id}")]
		public async Task<IHttpActionResult> UpdateEquipmentLocationHistory(int id, [FromBody] EquipmentLocationHistoryDTO equipmentLocationHistoryDTO)
		{
			try
			{
				Equipment_Location_History equipmentLocationHistory = mapper.Map<Equipment_Location_History>(equipmentLocationHistoryDTO);
				bool addNewSuccessfull = await equipmentLocationHistoryService.UpdateEquipmentLocationHistory(id, equipmentLocationHistory);

				if (addNewSuccessfull)
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
