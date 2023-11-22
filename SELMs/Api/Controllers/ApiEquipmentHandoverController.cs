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
using System.Web.Http;
using System.Web;

namespace SELMs.Api.Controllers
{
    [RoutePrefix("api/v1")]
    public class ApiEquipmentHandoverController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ApiEquipmentHandoverController));

        private IEquipmentHandoverFormRepository repository = new EquipmentHandoverFormRepository();
        private IEquipmentHandoverService service = new EquipmentHandoverService();
        private IMapper mapper = MapperConfig.Initialize();

        #region Get application by id
        [HttpPost]
        [Route("equipment-handover/forms")]
        public async Task<IHttpActionResult> GetEHFList(Argument arg)
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

        #region Get application by id
        [HttpGet]
        [Route("equipment-handover-form/{id}")]
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
        [Route("equipment-handover-form/new-form")]
        public async Task<IHttpActionResult> SaveApplication([FromBody] EquipmentHandoverRequest applicationRequest)
        {
            try
            {
                Equipment_Handover_Form application = mapper.Map<Equipment_Handover_Form>(applicationRequest.application);
                List<Equipment_Handover_Form_Detail> details = mapper.Map<List<Equipment_Handover_Form_Detail>>(applicationRequest.application_details);
                HttpPostedFileBase attachment = applicationRequest.attachment;
                service.SaveApplication(application, details, attachment);
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
        [Route("equipment-handover_form/update/{id}")]
        public async Task<IHttpActionResult> UpdateApplication(int id, [FromBody] EquipmentHandoverRequest applicationRequest)
        {
            try
            {
                Equipment_Handover_Form application = mapper.Map<Equipment_Handover_Form>(applicationRequest.application);
                List<Equipment_Handover_Form_Detail> details = mapper.Map<List<Equipment_Handover_Form_Detail>>(applicationRequest.application_details);
                HttpPostedFileBase attachment = applicationRequest.attachment;
                service.UpdateApplication(id, application, details, attachment);
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
