using System.Collections.Generic;
using System.Threading.Tasks;
using SELMs.Models;

namespace SELMs.Repositories
{
	public interface IMemberLocationHistoryRepository
	{
		Task<List<Member_Location_History>> GetMemberLocationHistoryList();
		Task<Member_Location_History> GetMemberLocationHistoryById(int id);
		dynamic AddMemberLocationHistory(Member_Location_History memberLocationHistory);
		Task<bool> UpdateMemberLocationHistory(Member_Location_History memberLocationHistory);
	}
}
