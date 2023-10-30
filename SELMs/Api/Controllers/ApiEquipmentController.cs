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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SELMs.Api.Controllers
{
    public class ApiEquipmentController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ApiEquipmentController));

        private IEquipmentRepository repository = new EquipmentRepository();
        private IEquipmentService service = new EquipmentService();
        private IMapper mapper = MapperConfig.Initialize();

        // GET: Api_Equipment
        #region Get equipment list
        [HttpGet]
        [Route("equipments")]
        public async Task<IHttpActionResult> GetEquipmentList()
        {
            try
            {
                dynamic returnedData = null;
                returnedData = await repository.GetEquipmentList();
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

        #region Search Equipment
        [HttpPost]
        [Route("equipments/search")]
        public async Task<IHttpActionResult> SearchEquipments(Argument args)
        {
            try
            {
                dynamic returnedData = null;
                returnedData = await repository.SearchEquipments(args);
                return Ok(returnedData);
            }
            catch (Exception ex)
            {
                Log.Error("Error: ", ex);
                Console.WriteLine($"{ex.Message} \n { ex.StackTrace}");
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
                returnedData = repository.GetEquipment(id);
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

        #region Add new equipment
        [HttpPost]
        [Route("api/equipments/new-equipment")]
        public async Task<IHttpActionResult> SaveEquipment(EquipmentDTO dto)
        {
            try
            {
                Equipment equipment = mapper.Map<Equipment>(dto);
                service.SaveEquipment(equipment);
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

        #region Delete Equipment
        [HttpDelete]
        [Route("equipments/delete/{id}")]
        public async Task<IHttpActionResult> DeleteEquipments(int id)
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
    }
}
