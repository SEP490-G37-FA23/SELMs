using System.Collections.Generic;
using System.Threading.Tasks;
using SELMs.Api.DTOs;
using SELMs.Models;
using SELMs.Models.BusinessModel;

namespace SELMs.Repositories
{
	public interface ILocationRepository
	{
		dynamic GetLocationList();
		dynamic GetLocationList(Argument arg);
		dynamic GetAllSubLocationList(Argument arg);
		dynamic GetLocation(int id);
		dynamic SaveLocation(Location location);
		dynamic SaveLocations(List<Location> locations);
		dynamic UpdateLocation(Location location);
		void DeleteLocation(int id);

		List<LocationDTO> GetListSubLocation(int id);
		List<ProjectDTO> GetListProjectInLocation(int id);
		List<EquipmentDTO> GetListEquipInLocation(int id);

		dynamic GetEquipment_Location_History(string system_equipment_code, int location_id);
		dynamic AddNewEquipLocationHistory(Equipment_Location_History item);
		void UpdateEquipLocationHistory(Equipment_Location_History item);
		List<UserDTO> GetListMemberInLocation(int id);
		Task<List<Location>> GetSublocationByParentId(int parentId);
		Task RemoveRange(List<Location> locations); // for sub-location
	}
}
