using System.Threading.Tasks;
using SELMs.Models;
using SELMs.Repositories;
using SELMs.Repositories.Implements;

namespace SELMs.Services.Implements
{
	public class MemberProjectHistoryService : IMemberProjectHistoryService
	{

		private readonly IMemberProjectHistoryRepository projectMemberHistoryRepository = new MemberProjectHistoryRepository();

		public async Task<bool> UpdateHistory(int id, Member_Project_History memberProjectHistory)
		{
			var historyFound = await projectMemberHistoryRepository.GetMemberProjectHistoryById(id);

			if (historyFound != null)
			{
				memberProjectHistory.id = id;
				return await projectMemberHistoryRepository.UpdateHistory(memberProjectHistory);
			}
			return false;
		}
	}
}