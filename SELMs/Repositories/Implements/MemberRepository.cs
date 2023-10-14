using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SELMs.Models;
using SELMs.Models.BusinessModel;

namespace SELMs.Repositories.Implements
{
	public class MemberRepository : IMemberRepository
	{
		private SELMsEntities db = new SELMsEntities();

		public dynamic DeleteMember(dynamic member)
		{
			throw new NotImplementedException();
		}

		public dynamic GetMember()
		{
			throw new NotImplementedException();
		}

		public async Task<dynamic> GetMemberByCodeOrUsername(string memberCodeOrUsername)
		{
			return await db.Users
				.Where(user => user.member_code.Equals(memberCodeOrUsername) || user.username.Equals(memberCodeOrUsername))
				.FirstOrDefaultAsync();
		}

		public dynamic GetMemberList(Argument args)
		{
			dynamic members = null;
			members =  db.Database.Connection.Query<dynamic>("Proc_GetMembersList", new
			{
				username = args.username,
			}
				, commandType: CommandType.StoredProcedure).ToList();
			return members;
		}

		public dynamic SaveMember(dynamic member)
		{

			throw new NotImplementedException();
		}

		public async Task<dynamic> UpdateMember()
		{
			return await db.SaveChangesAsync() > 0;
		}
	}
}