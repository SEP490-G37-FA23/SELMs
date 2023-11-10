using System.Threading.Tasks;
using SELMs.Models;

namespace SELMs.Services
{
	public interface IMemberProjectHistoryService
	{
		Task<bool> UpdateHistory(int id, Member_Project_History memberProjectHistory);
	}
}
