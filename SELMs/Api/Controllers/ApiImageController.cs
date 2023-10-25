using AutoMapper;
using log4net;
using SELMs.Api.DTOs;
using SELMs.App_Start;
using SELMs.Models;
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
    public class ApiImageController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ApiImageController));

        private IImageRepository repository = new ImageRepository();
        private IImageService service = new ImageService();
        private IMapper mapper = MapperConfig.Initialize();

        #region Get image by id
        [HttpGet]
        [Route("images/{id}")]
        public async Task<IHttpActionResult> GetImage(int id)
        {
            try
            {
                dynamic returnedData = null;
                returnedData = repository.GetImage(id);
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

        #region Add new image
        [HttpPost]
        [Route("api/images/new-image")]
        public async Task<IHttpActionResult> SaveImage(ImageDTO dto)
        {
            try
            {
                Image image = mapper.Map<Image>(dto);
                repository.SaveImage(image);
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
