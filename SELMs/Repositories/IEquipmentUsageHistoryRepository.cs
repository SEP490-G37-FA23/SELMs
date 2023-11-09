using System.Threading.Tasks;
using SELMs.Models;

namespace SELMs.Repositories
{
	public interface IEquipmentUsageHistoryRepository
	{

		Task<Equipment_Usage_History> GetEquipmentUsageHistoryById(int id);
		Task<bool> Add(Equipment_Usage_History equipmentUsageHistory);
		Task<bool> Update(Equipment_Usage_History equipmentUsageHistory);
	}
}
