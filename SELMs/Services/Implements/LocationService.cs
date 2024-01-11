using System.Collections.Generic;
using System.Threading.Tasks;
using SELMs.Models;
using SELMs.Repositories;
using SELMs.Repositories.Implements;

namespace SELMs.Services.Implements
{
	public class LocationService : ILocationService
	{
		private ILocationRepository repository = new LocationRepository();

		public async Task<dynamic> SaveLocation(Location location, List<Location> subLocations)
		{
			Location loc = repository.SaveLocation(location);
			foreach (Location subLocation in subLocations)
			{
				subLocation.parent_location_id = loc.location_id;
				subLocation.location_level = loc.location_level + 1;
			}
			repository.SaveLocations(subLocations);
			return loc;
		}

		public async Task<dynamic> UpdateLocation(int id, Location location)
		{
			if (repository.GetLocation(id) != null)
			{
				location.location_id = id;
				location = repository.UpdateLocation(location);
			}
			return location;
		}
		public async Task SaveEquipLocationHistory(Equipment_Location_History item)
		{
			if (repository.GetEquipment_Location_History(item.system_equipment_code, item.location_id) == null)
			{
				repository.AddNewEquipLocationHistory(item);
			}
			else
			{
				repository.UpdateEquipLocationHistory(item);
			}
		}

		public async Task UpdateLocationAndSublocation(int id, Location location, List<Location> subLocations)
		{
			location.location_id = id;

			// Update location
			repository.UpdateLocation(location);

			// delete all sub-location by parent_id
			List<Location> oldSubLocation = await repository.GetSublocationByParentId(id);
			await repository.RemoveRange(oldSubLocation);

			// add new sub-location again
			repository.SaveLocations(subLocations);
		}
	}
}