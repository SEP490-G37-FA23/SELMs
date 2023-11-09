using System.Data.Entity;
using System.Threading.Tasks;
using SELMs.Models;

namespace SELMs.Repositories.Implements
{
	public class EquipmentUsageHistoryRepository : IEquipmentUsageHistoryRepository
	{

		private readonly SELMsContext db = new SELMsContext();


		public async Task<bool> Add(Equipment_Usage_History equipmentUsageHistory)
		{
			db.Equipment_Usage_History.Add(equipmentUsageHistory);
			return (await db.SaveChangesAsync() > 0);
		}



		public async Task<Equipment_Usage_History> GetEquipmentUsageHistoryById(int id)
		{
			return await db.Equipment_Usage_History.FirstOrDefaultAsync(e => e.id == id);
		}



		public async Task<bool> Update(Equipment_Usage_History equipmentUsageHistory)
		{
			Equipment_Usage_History eph = await db.Equipment_Usage_History.FirstOrDefaultAsync(e => e.id == equipmentUsageHistory.id);

			db.Entry(eph).CurrentValues.SetValues(equipmentUsageHistory);

			return (await db.SaveChangesAsync() > 0);
		}
	}
}