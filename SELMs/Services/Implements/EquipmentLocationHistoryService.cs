using System.Threading.Tasks;
using SELMs.Models;
using SELMs.Repositories.Implements;

namespace SELMs.Services.Implements
{
	public class EquipmentLocationHistoryService : IEquipmentLocationHistoryService
	{

		private readonly IEquipmentLocationHistoryRepository equipmentLocationHistoryRepository = new EquipmentLocationHistoryRepository();



		public async Task<bool> UpdateEquipmentLocationHistory(int id, Equipment_Location_History equipmentLocationHistory)
		{
			var equipmentLocationHistoryFound = await equipmentLocationHistoryRepository.GetEquipmentLocationHistoryById(id);

			if (equipmentLocationHistoryFound != null)
			{
				equipmentLocationHistory.id = id;
				return await equipmentLocationHistoryRepository.Update(equipmentLocationHistory);
			}
			return false;
		}
	}
}