using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using SELMs.Models;

namespace SELMs.Repositories.Implements
{
	public class EquipmentProjectHistoryRepository : IEquipmentProjectHistoryRepository
	{

		private readonly SELMsContext db = new SELMsContext();

		public async Task<bool> Add(Equipment_Project_History equipmentProjectHistory)
		{
			db.Equipment_Project_History.Add(equipmentProjectHistory);
			return (await db.SaveChangesAsync() > 0);
		}



		public async Task<Equipment_Project_History> GetEquipmentProjectHistoryById(int id)
		{
			return await db.Equipment_Project_History.FirstOrDefaultAsync(e => e.id == id);
		}


		public async Task<List<Equipment_Project_History>> GetEquipmentProjectHistoryList()
		{
			return await db.Equipment_Project_History.ToListAsync();
		}


		public async Task<bool> Update(Equipment_Project_History equipmentProjectHistory)
		{
			Equipment_Project_History eph = await db.Equipment_Project_History.FirstOrDefaultAsync(e => e.id == equipmentProjectHistory.id);

			db.Entry(eph).CurrentValues.SetValues(equipmentProjectHistory);

			return (await db.SaveChangesAsync() > 0);
		}
	}
}