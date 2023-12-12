using AutoMapper;
using log4net;
using Newtonsoft.Json.Linq;
using SELMs.Api.DTOs;
using SELMs.App_Start;
using SELMs.Models;
using SELMs.Repositories;
using SELMs.Repositories.Implements;
using SELMs.Services;
using SELMs.Services.Implements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace SELMs.Api.Controllers
{
    [RoutePrefix("api/v1")]
    public class ApiImageController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ApiImageController));

        private IImageRepository repository = new ImageRepository();
        private IImageService service = new ImageService();
        private IMapper mapper = MapperConfig.Initialize();

        #region Get image by id
        [HttpGet]
        [Route("images/{id}")]
        public async Task<HttpResponseMessage> GetImage(int id)
        {
            try
            {
                Image image = repository.GetImage(id);
                HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new StreamContent(new MemoryStream(image.content));
                result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = $"{image.image_name}.png";
                result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
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

        #region Upload images
        [HttpPost]
        [Route("api/images/upload")]
        public async Task<IHttpActionResult> UploadImages()
        {
            try
            {
                var name = HttpContext.Current.Request.Params["name"];
                var files = HttpContext.Current.Request.Files.GetMultiple("files").ToList();
                foreach (var file in files)
                {
                    service.SaveImage(file, name);
                }
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

        #region Update image
        [HttpPut]
        [Route("images/{id}")]
        public async Task<IHttpActionResult> UpdateImage(int id, [FromBody] ImageDTO image)
        {
            try
            {
                Image mem = mapper.Map<Image>(image);
                service.UpdateImage(id, mem);
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
