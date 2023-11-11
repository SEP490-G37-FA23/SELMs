using System.Threading.Tasks;
using SELMs.Models;

namespace SELMs.Services
{
	public interface IEquipmentProjectHistoryService
	{
		Task<bool> UpdateEquipmentProjectHistory(int id, Equipment_Project_History equipmentProjectHistory);
	}
}
