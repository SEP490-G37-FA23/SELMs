using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using SELMs.Models;

namespace SELMs.Repositories.Implements
{
	public class MemberLocationHistoryRepository : IMemberLocationHistoryRepository
	{
		private readonly SELMsContext db = new SELMsContext();


		public async Task<bool> AddMemberLocationHistory(Member_Location_History memberLocationHistory)
		{
			db.Member_Location_History.Add(memberLocationHistory);
			return (await db.SaveChangesAsync() > 0);
		}


		public async Task<Member_Location_History> GetMemberLocationHistoryById(int id)
		{
			return await db.Member_Location_History.FirstOrDefaultAsync(m => m.id == id);
		}


		public async Task<List<Member_Location_History>> GetMemberLocationHistoryList()
		{
			return await db.Member_Location_History.ToListAsync();
		}


		public async Task<bool> UpdateMemberLocationHistory(Member_Location_History memberLocationHistory)
		{
			Member_Location_History orghistory = await db.Member_Location_History
				.FirstOrDefaultAsync(o => o.id == memberLocationHistory.id);

			db.Entry(orghistory).CurrentValues.SetValues(memberLocationHistory);
			return (await db.SaveChangesAsync() > 0);
		}
	}
}