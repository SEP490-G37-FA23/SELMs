﻿using AutoMapper;
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
using SELMs.Models.BusinessModel;
using System.Data.Entity.Core.Metadata.Edm;

namespace SELMs.Api.Controllers
{
    [RoutePrefix("api/v1")]
    public class ApiLocationController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ApiLocationController));

        private ILocationRepository repository = new LocationRepository();
        private ILocationService service = new LocationService();
        private IMapper mapper = MapperConfig.Initialize();

        #region Get locations list
        [HttpPost]
        [Route("all-locations")]
        public dynamic GetLocationList()
        {
            try
            {
                dynamic returnedData = null;
                returnedData = repository.GetLocationList();
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

        #region Get locations list by level
        [HttpPost]
        [Route("locations")]
        public dynamic GetLocationList(Argument args)
        {
            try
            {
                dynamic returnedData = null;
                returnedData = repository.GetLocationList(args);
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

        #region Get all sub locations list by id
        [HttpPost]
        [Route("locations/all-sub-location")]
        public dynamic GetAllSubLocationList(Argument args)
        {
            try
            {
                dynamic returnedData = null;
                returnedData = repository.GetAllSubLocationList(args);
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
        [Route("locations/new-location")]
        public async Task<IHttpActionResult> SaveLocation(LocationDTO dto)
        {
            try
            {
                Location location = mapper.Map<Location>(locationRequest.Location);
                List<Location> subLocations = mapper.Map<List<Location>>(locationRequest.SubLocations);
                service.SaveLocation(location, subLocations);
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
        [HttpPost]
        [Route("locations/update/{id}")]
        public async Task<IHttpActionResult> UpdateLocation(int id, [FromBody] LocationDTO location)
        {
            try
            {
                Location location = mapper.Map<Location>(locationRequest.Location);
                List<Location> subLocations = mapper.Map<List<Location>>(locationRequest.SubLocations);
                service.SaveLocation(location, subLocations);
                service.UpdateLocation(id, location, subLocations);
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

        #region Get detail location by location id
        [HttpPost]
        [Route("locations/detail/{id}")]
        public DetailLocationDTO GetDetailLocation(int id)
        {
            DetailLocationDTO returnedData = new DetailLocationDTO();
            returnedData.location_info = repository.GetLocation(id);
            returnedData.ListSubLocation = (List<LocationDTO>)repository.GetListSubLocation(id);
            returnedData.ListProjectInLocation = (List<ProjectDTO>)repository.GetListProjectInLocation(id);
            returnedData.ListEquipmentInLocation = (List<EquipmentDTO>)repository.GetListEquipInLocation(id);
            return returnedData;


        }
        #endregion

        #region Add equip in  location
        [HttpPost]
        [Route("locations/equip-location-history")]
        public async Task<IHttpActionResult> SaveEquipLocationHistory(EquipLocationHistoryDTO dto)
        {
            try
            {
                foreach(var item in dto.ListEquipLocationHistory)
                {
                    service.SaveEquipLocationHistory(item);
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
    }
}