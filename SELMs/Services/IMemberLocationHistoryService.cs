using System.Threading.Tasks;
using SELMs.Models;

namespace SELMs.Services
{
	public interface IMemberLocationHistoryService
	{

		Task<bool> UpdateHistory(int id, Member_Location_History memberLocationHistory);
	}
}
