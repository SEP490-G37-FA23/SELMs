using System.Threading.Tasks;
using SELMs.Models;
using SELMs.Repositories;
using SELMs.Repositories.Implements;

namespace SELMs.Services.Implements
{
	public class MemberLocationHistoryService : IMemberLocationHistoryService
	{

		private readonly IMemberLocationHistoryRepository memberLocationHistoryRepository = new MemberLocationHistoryRepository();

		public async Task<bool> UpdateHistory(int id, Member_Location_History memberLocationHistory)
		{
			var historyFound = await memberLocationHistoryRepository.GetMemberLocationHistoryById(id);

			if (historyFound != null)
			{
				memberLocationHistory.id = id;
				return await memberLocationHistoryRepository.UpdateMemberLocationHistory(memberLocationHistory);
			}
			return false;
		}
	}
}