using System.Collections.Generic;
using System.Threading.Tasks;
using SELMs.Models;

namespace SELMs.Repositories
{
	public interface IMemberProjectHistoryRepository
	{
		Task<List<Member_Project_History>> GetMemberProjectHistoryList();
		Task<Member_Project_History> GetMemberProjectHistoryById(int id);
		Task SaveHistory(Member_Project_History memberProjectHistory);
		Task<bool> UpdateHistory(Member_Project_History memberProjectHistory);
		Task<List<Member_Project_History>> GetMemberProjectHistoryListByProjectId(int id);
		Task RemoveAllMember(List<Member_Project_History> list);
	}
}
