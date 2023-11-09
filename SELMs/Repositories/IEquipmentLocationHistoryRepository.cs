using System.Collections.Generic;
using System.Threading.Tasks;
using SELMs.Models;

namespace SELMs.Repositories.Implements
{
	public interface IEquipmentLocationHistoryRepository
	{
		Task<List<Equipment_Location_History>> GetEquipmentLocationHistoryList();
		Task<Equipment_Location_History> GetEquipmentLocationHistoryById(int id);
		Task<bool> Add(Equipment_Location_History equipmentLocationHistory);
		Task<bool> Update(Equipment_Location_History equipmentLocationHistory);
	}
}
