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
        private SELMsContext db = new SELMsContext();

        public dynamic DeleteMember(dynamic member)
        {
            throw new NotImplementedException();
        }

        public dynamic GetMember(int id)
        {
            dynamic member = db.Users.Where(u => u.user_id == id).FirstOrDefault();
            return member;
        }

        public dynamic GetMemberList(Argument args)
        {
            dynamic members = null;
            members = db.Database.Connection.Query<dynamic>("Proc_GetMembersList", new
            {
                username = args.username,
            }
                , commandType: CommandType.StoredProcedure).ToList();
            return members;
        }

        public void SaveMember(dynamic member)
        {
            db.Users.Add(member);
            db.SaveChanges();
        }

        public void UpdateMember(dynamic member)
        {
            db.Entry<User>(member).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}