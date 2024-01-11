using System.Collections.Generic;
using System.Threading.Tasks;
using SELMs.Models;

namespace SELMs.Services
{
	public interface ILocationService
	{
		Task<dynamic> SaveLocation(Location location, List<Location> subLocations);
		Task<dynamic> UpdateLocation(int id, Location location);
		Task SaveEquipLocationHistory(Equipment_Location_History item);
		Task UpdateLocationAndSublocation(int id, Location location, List<Location> subLocations);
	}
}
