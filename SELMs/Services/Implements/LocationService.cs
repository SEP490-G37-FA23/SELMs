﻿using System.Collections.Generic;
using System.Threading.Tasks;
using SELMs.Models;
using SELMs.Repositories;
using SELMs.Repositories.Implements;

namespace SELMs.Services.Implements
{
	public class LocationService : ILocationService
	{
		private ILocationRepository repository = new LocationRepository();

		public async Task SaveLocation(Location location, List<Location> subLocations)
		{
			Location loc = repository.SaveLocation(location);
			foreach (Location subLocation in subLocations)
			{
				subLocation.parent_location_id = loc.location_id;
				subLocation.location_level = loc.location_level + 1;
			}
			repository.SaveLocations(subLocations);
		}

		public async Task UpdateLocation(int id, Location location, List<Location> subLocations)
		{
			if (repository.GetLocation(id) != null)
			{
				location.location_id = id;
				repository.UpdateLocation(location);
				foreach (Location item in subLocations)
				{
					if (item.location_id == null || item.location_level == null)
					{
						repository.SaveLocation(item);
					}
					else
					{
						repository.UpdateLocation(item);
					}
				}
			}
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
	}
}