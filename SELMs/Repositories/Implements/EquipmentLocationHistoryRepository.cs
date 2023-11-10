using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using SELMs.Models;

namespace SELMs.Repositories.Implements
{
	public class EquipmentLocationHistoryRepository : IEquipmentLocationHistoryRepository
	{

		private readonly SELMsContext db = new SELMsContext();



		public async Task<bool> Add(Equipment_Location_History equipmentLocationHistory)
		{
			db.Equipment_Location_History.Add(equipmentLocationHistory);
			return (await db.SaveChangesAsync() > 0);
		}



		public async Task<Equipment_Location_History> GetEquipmentLocationHistoryById(int id)
		{
			return await db.Equipment_Location_History.FirstOrDefaultAsync(e => e.id == id);
		}


		public async Task<List<Equipment_Location_History>> GetEquipmentLocationHistoryList()
		{
			return await db.Equipment_Location_History.ToListAsync();
		}


		public async Task<bool> Update(Equipment_Location_History equipmentLocationHistory)
		{
			Equipment_Location_History elh = await db.Equipment_Location_History.FirstOrDefaultAsync(e => e.id == equipmentLocationHistory.id);
			db.Entry(elh).CurrentValues.SetValues(equipmentLocationHistory);
			return (await db.SaveChangesAsync() > 0);
		}
	}
}