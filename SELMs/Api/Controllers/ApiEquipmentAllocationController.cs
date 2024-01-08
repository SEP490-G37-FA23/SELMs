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
    [RoutePrefix("api/v1")]
    public class ApiEquipmentAllocationController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ApiEquipmentAllocationController));

        private IEquipmentAllocationApplicationRepository repository = new EquipmentAllocationApplicationRepository();
        private IEquipmentAllocationService service = new EquipmentAllocationService();
        private IMapper mapper = MapperConfig.Initialize();

        #region Get application by id
        [HttpPost]
        [Route("equipment-allocation-application/search")]
        public async Task<IHttpActionResult> GetEAAList(Argument arg)
        {
            try
            {
                dynamic returnedData = null;
                returnedData = repository.GetEAAList(arg);
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
        [Route("equipment-allocation-application/{id}")]
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
        [Route("equipment-allocation-application/new-application")]
        public async Task<IHttpActionResult> SaveApplication([FromBody] EquipmentAllocationApplicationRequest applicationRequest)
        {
            try
            {
                Equipment_Allocation_Application application = mapper.Map<Equipment_Allocation_Application>(applicationRequest.application);
                List<Equipment_Allocation_Application_Detail> details = mapper.Map<List<Equipment_Allocation_Application_Detail>>(applicationRequest.application_details);
                service.SaveApplication(application, details);
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
        [Route("equipment-allocation-application/new-application")]
        public async Task<IHttpActionResult> UpdateApplication(int id, [FromBody] EquipmentAllocationApplicationRequest applicationRequest)
        {
            try
            {
                Equipment_Allocation_Application application = mapper.Map<Equipment_Allocation_Application>(applicationRequest.application);
                List<Equipment_Allocation_Application_Detail> details = mapper.Map<List<Equipment_Allocation_Application_Detail>>(applicationRequest.application_details);
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

        #region Get aavaiable-equipments
        [HttpPost]
        [Route("equipment-allocation-application/avaiable-equipments")]
        public async Task<IHttpActionResult> GetAvailableEquipmentList(Argument arg)
        {
            try
            {
                dynamic returnedData = null;
                returnedData = repository.GetAvailableEquipmentList(arg);
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

        #region Get need import equipments
        [HttpPost]
        [Route("equipment-allocation-application/need-import-equipments")]
        public async Task<IHttpActionResult> GetNeedImportEquipmentList(Argument arg)
        {
            try
            {
                dynamic returnedData = null;
                returnedData = repository.GetNeedImportEquipmentList(arg);
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

        #region Approve Application
        [HttpPost]
        [Route("equipment-allocation-application/approve/{eaaCode}")]
        public async Task<IHttpActionResult> ApproveApplication([FromUri]string eaaCode)
        {
            try
            {
                dynamic returnedData = null;
                returnedData = await service.ApproveApplication(eaaCode);
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
    }
}
