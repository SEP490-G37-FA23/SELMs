﻿using AutoMapper;
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
using System.Web.ModelBinding;

namespace SELMs.Api.Controllers
{
    [RoutePrefix("api/v1")]
    public class ApiEquipmentHandoverController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ApiEquipmentHandoverController));

        private IEquipmentHandoverFormRepository repository = new EquipmentHandoverFormRepository();
        private IEquipmentHandoverService service = new EquipmentHandoverService();
        private IMapper mapper = MapperConfig.Initialize();

        #region Get application
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
                dynamic returnedData = service.GetApplication(id);
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
        [Route("equipment-handover_form/update/{id}")]
        public async Task<IHttpActionResult> UpdateApplication(int id, [FromBody] EquipmentHandoverRequest applicationRequest)
        {
            try
            {
                Equipment_Handover_Form application = mapper.Map<Equipment_Handover_Form>(applicationRequest.application);
                List<Equipment_Handover_Form_Detail> details = mapper.Map<List<Equipment_Handover_Form_Detail>>(applicationRequest.application_details);
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

        #region Attach file to application
        [HttpPost]
        [Route("equipment-handover/finish-file/")]
        public async Task<IHttpActionResult> AttachFile()
        {
            try
            {
                var result = new List<dynamic>();
                var applicationId = HttpContext.Current.Request.Params["application_id"];
                var files = HttpContext.Current.Request.Files.GetMultiple("attachments");
                foreach (var file in files)
                {
                    var item = await service.AddAttachment(Convert.ToInt32(applicationId), file);
                    result.Add(item);
                }
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

        #region Remove Attachment
        [HttpPost]
        [Route("equipment-handover/remove-attachment")]
        public async Task<IHttpActionResult> AttachFile(int application_id, int attach_id)
        {
            try
            {
                service.DeleteAttachment(application_id, attach_id);
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
        [Route("equipment-handover/confirm/{id}")]
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

        #region Cancel application
        [HttpPost]
        [Route("equipment-handover/cancel/{id}")]
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

    }
}
