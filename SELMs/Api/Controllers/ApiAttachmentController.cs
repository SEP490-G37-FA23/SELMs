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
using System.Web;
using System.IO;

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
        public async Task<HttpResponseMessage> GetAttachment(int id)
        {
            try
            {
                Attachment attachment = repository.GetAttachment(id);
                HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.OK);
                if (attachment != null)
                {                    
                    result.Content = new StreamContent(new MemoryStream(attachment.content));
                    result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                    result.Content.Headers.ContentDisposition.FileName = $"{attachment.name}{attachment.extension}";
                    result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                }
                return result;
            }
            catch (Exception ex)
            {
                Log.Error("Error: ", ex);
                Console.WriteLine($"{ex.Message} \n {ex.StackTrace}");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
                throw;
            }
        }
        #endregion

        #region Upload attachments
        [HttpPost]
        [Route("attachments/upload")]
        public async Task<IHttpActionResult> UploadAttachments()
        {
            try
            {
                var result = new List<dynamic>();
                var files = HttpContext.Current.Request.Files.GetMultiple("files").ToList();
                foreach (var file in files)
                {
                    result.Add(await service.SaveAttachment(file));
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

    }
}
