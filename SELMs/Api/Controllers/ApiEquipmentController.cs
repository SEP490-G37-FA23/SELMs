using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using log4net;
using SELMs.Api.DTOs;
using SELMs.App_Start;
using SELMs.Models;
using SELMs.Models.BusinessModel;
using SELMs.Repositories;
using SELMs.Repositories.Implements;
using SELMs.Services;
using SELMs.Services.Implements;

namespace SELMs.Api.Controllers
{
	[RoutePrefix("api/v1")]
	public class ApiEquipmentController : ApiController
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(ApiEquipmentController));

		private IEquipmentRepository repository = new EquipmentRepository();
		private IEquipmentService service = new EquipmentService();
		private IMapper mapper = MapperConfig.Initialize();

		// GET: Api_Equipment
		#region Get equipment list       
		[HttpPost]
		[Route("equipments")]
		public async Task<IHttpActionResult> GetEquipmentList(Argument args)
		{
			try
			{
				dynamic returnedData = null;
				returnedData = await repository.GetEquipmentList(args);
				return Ok(returnedData);
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

        #region Get equipment list       
        [HttpPost]
        [Route("standard-equipments")]
        public dynamic GetStandardEquipmentList(Argument args)
        {
            try
            {
                dynamic returnedData = null;
                returnedData =  repository.GetStandardEquipmentList(args);
                return Ok(returnedData);
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

        #region Get equipment by id
        [HttpGet]
		[Route("equipments/{id}")]
		public async Task<IHttpActionResult> GetEquipment(int id)
		{
			try
			{
				dynamic returnedData = null;
				returnedData = service.GetEquipment(id);
				return Ok(returnedData);
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

		#region Get detail equipment by system code
		[HttpPost]
		[Route("equipments/detail/{code}")]
		public async Task<DetailEquipDTO> GetDetailEquipment(string code)
		{
			DetailEquipDTO returnedData = new DetailEquipDTO();
			returnedData.equip = await repository.GetDetailEquipment(code);
			returnedData.ListComponentEquips =  repository.GetListComponentEquips(code);
			return returnedData;


		}
		#endregion

		#region Add new equipment

		[HttpPost]
		[Route("equipments/new-equipment")]
		public async Task<IHttpActionResult> SaveEquipment(EquipmentNew data)
		{
			try
			{
				Equipment equipment = mapper.Map<Equipment>(data.equip);
				service.SaveEquipment(equipment, data.location_id, data.ListComponentEquips, data.images);
				return Ok();
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

		#region Delete equipment by id
		[HttpGet]
		[Route("equipments/delete/{id}")]
		public async Task<IHttpActionResult> DeleteEquipment(int id)
		{
			try
			{
				repository.DeleteEquipment(id);
				return Ok();
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

		#region Import equipments
		[HttpPost]
		[Route("api/equipments/import-equipments")]
		public async Task<IHttpActionResult> ImportEquipments(List<EquipmentDTO> dtos)
		{
			try
			{
				List<Equipment> equipments = mapper.Map<List<Equipment>>(dtos);
				service.ImportEquipments(equipments);
				return Ok();
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

		#region Update equipment
		[HttpPut]
		[Route("equipments/{id}")]
		public async Task<IHttpActionResult> UpdateEquipment(int id, [FromBody] EquipmentDTO equipment)
		{
			try
			{
				Equipment mem = mapper.Map<Equipment>(equipment);
				service.UpdateEquipment(id, mem);
				return Ok();
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
