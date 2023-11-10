using System.Threading.Tasks;
using SELMs.Models;

namespace SELMs.Services
{
	public interface IEquipmentLocationHistoryService
	{
		Task<bool> UpdateEquipmentLocationHistory(int id, Equipment_Location_History equipmentLocationHistory);
	}
}
