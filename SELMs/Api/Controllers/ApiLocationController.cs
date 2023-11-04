using AutoMapper;
using log4net;
using SELMs.App_Start;
using SELMs.Repositories.Implements;
using SELMs.Repositories;
using SELMs.Services.Implements;
using SELMs.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using SELMs.Api.DTOs;
using SELMs.Models;

namespace SELMs.Api.Controllers
{
    [RoutePrefix("api/v1")]
    public class ApiLocationController: ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ApiLocationController));

        private ILocationRepository repository = new LocationRepository();
        private ILocationService service = new LocationService();
        private IMapper mapper = MapperConfig.Initialize();

        #region Get locations list
        [HttpPost]
        [Route("locations")]
        public dynamic GetLocationList()
        {
            try
            {
                dynamic returnedData = null;
                returnedData =  repository.GetLocationList();
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

        #region Get location by id
        [HttpGet]
        [Route("locations/{id}")]
        public async Task<IHttpActionResult> GetLocation(int id)
        {
            try
            {
                dynamic returnedData = null;
                returnedData = repository.GetLocation(id);
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

        #region Add new location
        [HttpPost]
        [Route("api/locations/new-location")]
        public async Task<IHttpActionResult> SaveLocation(LocationDTO dto)
        {
            try
            {
                Location location = mapper.Map<Location>(dto);
                service.SaveLocation(location);
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

        #region Update location
        [HttpPut]
        [Route("locations/{id}")]
        public async Task<IHttpActionResult> UpdateLocation(int id, [FromBody] LocationDTO location)
        {
            try
            {
                Location mem = mapper.Map<Location>(location);
                service.UpdateLocation(id, mem);
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

        #region Delete Location
        [HttpDelete]
        [Route("locations/delete/{id}")]
        public async Task<IHttpActionResult> DeleteLocations(int id)
        {
            try
            {
                repository.DeleteLocation(id);
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