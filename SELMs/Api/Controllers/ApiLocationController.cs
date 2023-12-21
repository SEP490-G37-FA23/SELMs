using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
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
		public async Task<IHttpActionResult> GetLocationList()
		{
			try
			{
				dynamic returnedData = null;
				returnedData = await repository.GetLocationList();
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
		public async Task<IHttpActionResult> GetLocationList(Argument args)
		{
			try
			{
				dynamic returnedData = null;
				returnedData = await repository.GetLocationList(args);
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
		public async Task<IHttpActionResult> GetAllSubLocationList(Argument args)
		{
			try
			{
				dynamic returnedData = null;
				returnedData = await repository.GetAllSubLocationList(args);
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
		public async Task<IHttpActionResult> SaveLocation([FromBody] LocationRequest locationRequest)
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
		public async Task<IHttpActionResult> UpdateLocation(int id, [FromBody] LocationDTO dto)
		{
			try
			{
				Location location = mapper.Map<Location>(dto);
				dynamic result = service.UpdateLocation(id, location);
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

		#region Delete Location
		[HttpPost]
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
            returnedData.ListMemberInLocation = (List<UserDTO>)repository.GetListMemberInLocation(id);
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
				List<dynamic> result = new List<dynamic>();
				foreach (var history in dto.ListEquipLocationHistory)
				{
					var item = service.SaveEquipLocationHistory(history);
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
	}
}