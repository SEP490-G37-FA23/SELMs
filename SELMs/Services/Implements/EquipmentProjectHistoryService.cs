using System.Threading.Tasks;
using SELMs.Models;
using SELMs.Repositories;
using SELMs.Repositories.Implements;

namespace SELMs.Services.Implements
{
	public class EquipmentProjectHistoryService : IEquipmentProjectHistoryService
	{

		private readonly IEquipmentProjectHistoryRepository repository = new EquipmentProjectHistoryRepository();

		public async Task<bool> UpdateEquipmentProjectHistory(int id, Equipment_Project_History equipmentProjectHistory)
		{
			Equipment_Project_History equipmentFound = await repository.GetEquipmentProjectHistoryById(id);

			if (equipmentFound != null)
			{
				equipmentProjectHistory.id = id;
				return await repository.Update(equipmentProjectHistory);
			}

			return false;
		}
	}
}