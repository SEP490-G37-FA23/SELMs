using System.Threading.Tasks;
using SELMs.Models.BusinessModel;

namespace SELMs.Repositories
{
	public interface IMemberRepository
	{
		dynamic GetMemberList(Argument arg);
		dynamic GetMember();
		dynamic SaveMember(dynamic member);
		Task<dynamic> UpdateMember();
		dynamic DeleteMember(dynamic member);
		Task<dynamic> GetMemberByCodeOrUsername(string memberCodeOrUsername);


	}
}
