using AutoMapper;
using log4net;
using SELMs.Api.DTOs;
using SELMs.App_Start;
using SELMs.Models.BusinessModel;
using SELMs.Models;
using SELMs.Repositories.Implements;
using SELMs.Repositories;
using SELMs.Services.Implements;
using SELMs.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using SELMs.Api.DTOs.Inventory;

namespace SELMs.Api.Controllers
{
    [RoutePrefix("api/v1")]
    public class ApiInventoryRequestController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ApiInventoryRequestController));

        private IInventoryRequestApplicationRepository repository = new InventoryRequestApplicationRepository();
        private IInventoryRequestService service = new InventoryRequestService();
        private IMapper mapper = MapperConfig.Initialize();

        #region Get application
        [HttpPost]
        [Route("inventory-request")]
        public async Task<IHttpActionResult> GetIRAList(Argument arg)
        {
            try
            {
                dynamic returnedData = null;
                returnedData = repository.GetApplicationList(arg);
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

        #region Get application detail-in-location
        [HttpPost]
        [Route("inventory/detail-in-location/{location_id}")]
        public async Task<IHttpActionResult> GetDetailIRAListInLocation(int location_id, Argument arg)
        {
            try
            {
                dynamic returnedData = null;
                returnedData = repository.GetDetailIRAListInLocation(location_id, arg);
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


        #region Get application by id
        [HttpGet]
        [Route("inventory-request/{id}")]
        public async Task<IHttpActionResult> GetApplication(int id)
        {
            try
            {
                dynamic returnedData = null;
                returnedData = repository.GetApplication(id);
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

        #region Add new application
        [HttpPost]
        [Route("inventory-request/new-application")]
        public async Task<IHttpActionResult> SaveApplication([FromBody] InventoryRequestRequest applicationRequest)
        {
            try
            {
                Inventory_Request_Application application = mapper.Map<Inventory_Request_Application>(applicationRequest.application);
                service.SaveApplication(application, applicationRequest.application_details);
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

        #region Update application
        [HttpPut]
        [Route("inventory-request/update/{id}")]
        public async Task<IHttpActionResult> UpdateApplication(int id, [FromBody] InventoryRequestRequest applicationRequest)
        {
            try
            {
                Inventory_Request_Application application = mapper.Map<Inventory_Request_Application>(applicationRequest.application);
                List<Inventory_Request_Application_Detail> details = mapper.Map<List<Inventory_Request_Application_Detail>>(applicationRequest.application_details);
                service.UpdateApplication(id, application, details);
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

        #region Confirm application
        [HttpPost]
        [Route("inventory-request/confirm/{id}")]
        public async Task<IHttpActionResult> ConfirmApplication(int id, [FromBody] UserDTO member)
        {
            try
            {
                User user = mapper.Map<User>(member);
                dynamic result = service.ConfirmApplication(id, user);
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

        #region Perform application
        [HttpPost]
        [Route("inventory-request/perform")]
        public async Task<dynamic> PerformInventoryRequest([FromBody] PerformInventoryDTO perform)
        {
            try
            {
                return await service.PerformInventoryRequest(perform);
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

        #region Perform application
        [HttpPost]
        [Route("inventory-request/update-equipment-result")]
        public async Task<dynamic> UpdateEquipmentResult([FromBody] UpdateEquipmentResultDTO updateEquip)
        {
            try
            {
                return await service.UpdateEquipmentResult(updateEquip);
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


        #region Cancel application
        [HttpPost]
        [Route("inventory-request/cancel/{id}")]
        public async Task<IHttpActionResult> CancelApplication(int id)
        {
            try
            {
                service.CancelApplication(id);
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

        #region Get application result
        [HttpPost]
        [Route("inventory/result")]
        public async Task<IHttpActionResult> GetResultIRAList(Argument arg)
        {
            try
            {
                dynamic returnedData = null;
                returnedData = await repository.GetResultIRAList(arg);
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

        #region Update result
        [HttpPost]
        [Route("inventory-request/equipment-result/update")]
        public async Task<IHttpActionResult> UpdateIRAListResult([FromBody] UpdateEquipmentResultDTO dto)
        {
            try
            {
                var result = service.UpdateEquipmentResult(dto);
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

        #region Delete result
        [HttpPost]
        [Route("inventory-request/detail/delete/{id}")]
        public async Task<IHttpActionResult> DeleteIRAResult(int id)
        {
            try
            {
                repository.DeleteApplicationDetail(id);
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

        #region Delete all result
        [HttpPost]
        [Route("inventory-request/detail/delete/all")]
        public async Task<IHttpActionResult> DeleteAllIRAResults()
        {
            try
            {
                repository.DeleteAllApplicationDetails();
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
