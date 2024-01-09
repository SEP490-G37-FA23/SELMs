using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SELMs.Models;

namespace SELMs.Repositories.Implements
{
	public class MemberProjectHistoryRepository : IMemberProjectHistoryRepository
	{

		private readonly SELMsContext db = new SELMsContext();

		public async Task<Member_Project_History> GetMemberProjectHistoryById(int id)
		{
			return await db.Member_Project_History.FirstOrDefaultAsync(m => m.id == id);
		}


		public async Task<List<Member_Project_History>> GetMemberProjectHistoryList()
		{
			return await db.Member_Project_History.ToListAsync();
		}


		public async Task SaveHistory(Member_Project_History memberProjectHistory)
		{
			db.Member_Project_History.Add(memberProjectHistory);
			await db.SaveChangesAsync();
		}



		public async Task<bool> UpdateHistory(Member_Project_History memberProjectHistory)
		{
			Member_Project_History orgMemberProjectHistory = await db.Member_Project_History
				.Where(m => m.id == memberProjectHistory.id)
				.FirstOrDefaultAsync();

			db.Entry(orgMemberProjectHistory).CurrentValues.SetValues(memberProjectHistory);

			return (await db.SaveChangesAsync() > 0);
		}


		public async Task<List<Member_Project_History>> GetMemberProjectHistoryListByProjectId(int id)
		{
			return (await db.Member_Project_History.Where(m => m.project_id == id).ToListAsync());
		}

		public async Task RemoveAllMember(List<Member_Project_History> list)
		{
			db.Member_Project_History.RemoveRange(list);
			await db.SaveChangesAsync();
		}
	}
}