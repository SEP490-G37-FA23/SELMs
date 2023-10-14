using System.Threading.Tasks;

namespace SELMs.Services
{
	public interface IMemberService
	{
		Task<dynamic> MarkMemberQuit(dynamic member);

		Task<dynamic> UpdateMember(dynamic member);
		//Task<dynamic> GetMemberByUsername(string username);
	}
}
