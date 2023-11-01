using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Security;
using Dapper;
using SELMs.Models;
using SELMs.Models.BusinessModel;
using static System.Net.Mime.MediaTypeNames;

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


        public dynamic GetMemberList(Argument arg)
        {
            dynamic members = null;
            members = db.Database.Connection.QueryAsync<dynamic>("Proc_GetMembersList", new
            {
                username = arg.username,
                isadmin = arg.isadmin,
                role = arg.role,
                text = arg.text
            }
                , commandType: CommandType.StoredProcedure);
            return members;
        }

        public dynamic SaveMember(User member)
        {
            db.Users.Add(member);
            db.SaveChangesAsync();
            return member;
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

        public dynamic GetRoleList()
        {
            dynamic members = null;
            members = db.Database.Connection.QueryAsync<dynamic>("Proc_GetRolesList"
                , commandType: CommandType.StoredProcedure);
            return members;
        }
    }
}