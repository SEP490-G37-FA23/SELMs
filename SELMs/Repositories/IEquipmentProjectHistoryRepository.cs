using System.Collections.Generic;
using System.Threading.Tasks;
using SELMs.Models;

namespace SELMs.Repositories
{
	public interface IEquipmentProjectHistoryRepository
	{
		Task<List<Equipment_Project_History>> GetEquipmentProjectHistoryList();
		Task<Equipment_Project_History> GetEquipmentProjectHistoryById(int id);
		Task<bool> Add(Equipment_Project_History equipmentProjectHistory);
		Task<bool> Update(Equipment_Project_History equipmentProjectHistory);
	}
}
