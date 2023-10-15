using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SELMs.Models;
using SELMs.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SELMs.Repositories.Implements
{
	public class MemberRepository : IMemberRepository
	{
		private SELMsEntities db = new SELMsEntities();

		public dynamic DeleteMember(dynamic member)
		{
			throw new NotImplementedException();
		}

        public dynamic GetMember(int id)
        {
            dynamic member = db.Users.Where(u => u.user_id == id).FirstOrDefaultAsync();
            return member;
        }

        public dynamic GetMemberList(Argument args)
        {
            dynamic members = db.Users.ToListAsync();
            
            return members;
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
        public dynamic SearchMembers(Argument arg)
        {
            dynamic members = null;
            members = db.Database.Connection.QueryAsync<dynamic>("Proc_GetMembersList", new
            {
                username = arg.username,
            }
                , commandType: CommandType.StoredProcedure);
            return members;
        }

        public void SaveMember(dynamic member)
        {
            db.Users.Add(member);
            db.SaveChangesAsync();
        }

        public void UpdateMember(dynamic member)
        {
            db.Entry<User>(member).State = EntityState.Modified;
            db.SaveChangesAsync();
        }

        public string GetLastMemberCode(string prefix)
        {
            User obj = db.Users.Where(u => u.member_code.StartsWith(prefix)).OrderBy(u => u.member_code).Last();
            string result = obj.member_code;
            return result;
        }
    }
}