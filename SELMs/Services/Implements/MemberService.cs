using System.Threading.Tasks;
using SELMs.Models;
using SELMs.Repositories;
using SELMs.Repositories.Implements;

namespace SELMs.Services
{
	public class MemberService : IMemberService
	{

		private readonly IMemberRepository memberRepository = new MemberRepository();

		public async Task<dynamic> MarkMemberQuit(dynamic member)
		{

			// (member.isActive = 0)

			return await memberRepository.UpdateMember();
		}

		public async Task<dynamic> UpdateMember(dynamic memberToUpdate)
		{


			dynamic memberFound = await memberRepository.GetMemberByCodeOrUsername((memberToUpdate as User).member_code);

			if (memberFound != null)
			{
				// map DTO later for member properties
				// memberFound = mapper.Map<User>(memberToUpdate)
			}

			return await memberRepository.UpdateMember();
		}
	}
}