using System.Threading.Tasks;
using SELMs.Models;
using SELMs.Repositories.Implements;

namespace SELMs.Services.Implements
{
	public class EquipmentLocationHistoryService : IEquipmentLocationHistoryService
	{

		private readonly IEquipmentLocationHistoryRepository repository = new EquipmentLocationHistoryRepository();



		public async Task<bool> UpdateEquipmentLocationHistory(int id, Equipment_Location_History equipmentLocationHistory)
		{
			var equipmentLocationHistoryFound = await repository.GetEquipmentLocationHistoryById(id);

			if (equipmentLocationHistoryFound != null)
			{
				equipmentLocationHistory.id = id;
				return await repository.Update(equipmentLocationHistory);
			}
			return false;
		}

		public async Task<dynamic> AddEquipmentLocationHistory(string equipmentSysCode, int locationId)
		{
			Equipment_Location_History history = new Equipment_Location_History()
			{
				from_date = System.DateTime.Now,
				to_date = System.DateTime.Now.AddDays(7),
				location_id = locationId,
				system_equipment_code = equipmentSysCode,
				note = ""
			};
			var result = repository.Add(history);
			return result;
		}

    }
}