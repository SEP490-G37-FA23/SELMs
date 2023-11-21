using AutoMapper;
using log4net;
using SELMs.Api.DTOs;
using SELMs.App_Start;
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
using SELMs.Models;

namespace SELMs.Api.Controllers
{
    [RoutePrefix("api/v1")]
    public class ApiAttachmentController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ApiAttachmentController));

        private IAttachmentRepository repository = new AttachmentRepository();
        private IAttachmentService service = new AttachmentService();
        private IMapper mapper = MapperConfig.Initialize();

        #region Get attachment by id
        [HttpGet]
        [Route("attachments/{id}")]
        public async Task<IHttpActionResult> GetAttachment(int id)
        {
            try
            {
                dynamic returnedData = null;
                returnedData = repository.GetAttachment(id);
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

        #region Add new attachment
        [HttpPost]
        [Route("api/attachments/new-attachment")]
        public async Task<IHttpActionResult> SaveAttachment(AttachmentDTO dto)
        {
            try
            {
                Attachment attachment = mapper.Map<Attachment>(dto);
                repository.SaveAttachment(attachment);
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

        #region Update attachment
        [HttpPut]
        [Route("attachments/{id}")]
        public async Task<IHttpActionResult> UpdateAttachment(int id, [FromBody] AttachmentDTO attachment)
        {
            try
            {
                Attachment mem = mapper.Map<Attachment>(attachment);
                service.UpdateAttachment(id, mem);
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
