using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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

        #region GetStandardEquipmentList    
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
				service.SaveEquipment(equipment, data.location_id, data.ListComponentEquips);
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
		[Route("equipments/import-equipments")]
		public async Task<IHttpActionResult> ImportEquipments(EquipmentImportDTO dto)
		{
			try
			{
				List<Equipment> equipments = mapper.Map<List<Equipment>>(dto.ListEquipImport);
				service.ImportEquipments(equipments, dto.username);
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
		[HttpPost]
		[Route("equipments/update/{id}")]
		public async Task<IHttpActionResult> UpdateEquipment(int id, [FromBody] EquipmentDTO equipment)
		{
			try
			{
				Equipment mem = mapper.Map<Equipment>(equipment);
				dynamic result = service.UpdateEquipment(id, mem);
				return Ok(result);
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

        #region Add Images
    //    [HttpPost]
    //    [Route("equipments/images/add")]
    //    public async Task<IHttpActionResult> AddImages()
    //    {
    //        try
    //        {
    //            var equipmentId = HttpContext.Current.Request.Params["equipment_id"];
    //            var files = HttpContext.Current.Request.Files.GetMultiple("images").ToList();
				//service.AddImages(Convert.ToInt32(equipmentId), files);
    //            return Ok();
    //        }
    //        catch (Exception ex)
    //        {
    //            Log.Error("Error: ", ex);
    //            Console.WriteLine($"{ex.Message} \n {ex.StackTrace}");
    //            return BadRequest($"{ex.Message} \n {ex.StackTrace}");
    //            throw;
    //        }
    //    }
        #endregion

        [HttpPost]
        [Route("equipments/images/add")]
        public IHttpActionResult AddImages()
        {
            try
            {

                // Retrieve equipment_id from the request
                var equipmentId = HttpContext.Current.Request.Form["equipment_id"];

                // Convert equipment_id to an integer (you might want to add error handling here)
                int equipmentIdInt = Convert.ToInt32(equipmentId);

                // Retrieve the images from the request
                var files = HttpContext.Current.Request.Files.GetMultiple("images[]");

                // Assuming there's a service class with the AddImages method
                // AddImages method should handle the logic to save/process images
                var fileList = files.ToList();

                // Assuming there's a service class with the AddImages method
                // AddImages method should handle the logic to save/process images
                service.AddImages(equipmentIdInt, fileList);

                // Return a successful response
                return Ok("Images added successfully");

            }
            catch (Exception ex)
            {
                // Log the exception
                Log.Error("Error: ", ex);

                // Return a bad request response with the error message
                return BadRequest($"{ex.Message} \n {ex.StackTrace}");
            }
        }
    }
}
