using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Dapper;
using SELMs.Models;
using SELMs.Models.BusinessModel;

namespace SELMs.Repositories.Implements
{
    public class MemberRepository : IMemberRepository
    {
        private SELMsContext db = new SELMsContext();

        public dynamic DeleteMember(User member)
        {
            throw new NotImplementedException();
        }

        public dynamic GetMember(int id)
        {
            dynamic member = db.Users.Where(u => u.user_id == id).FirstOrDefaultAsync();
            return member;
        }

        public dynamic GetMemberList()
        {
            dynamic members = db.Users.ToListAsync();

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

        public void SaveMember(User member)
        {
            db.Users.Add(member);
            db.SaveChangesAsync();
        }

        public void UpdateMember(User member)
        {
            User orgMember = db.Users.Where(u => u.user_id == member.user_id).FirstOrDefault();
            db.Entry(orgMember).CurrentValues.SetValues(member);
            db.SaveChangesAsync();
        }

        public string GetLastMemberCode(string prefix)
        {
            User obj = db.Users.Where(u => u.user_code.StartsWith(prefix)).OrderByDescending(u => u.user_code).FirstOrDefault();
            if (obj != null)
            {
                string result = obj.user_code == null ? "" : obj.user_code;
                return result;
            }
            return "";
        }
    }
}