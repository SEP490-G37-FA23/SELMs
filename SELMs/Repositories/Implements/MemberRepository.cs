using Dapper;
using SELMs.Models;
using SELMs.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Data;
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

        public dynamic GetMember()
        {
            throw new NotImplementedException();
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

        public dynamic SaveMember(dynamic member)
        {

            throw new NotImplementedException();
        }

        public dynamic UpdateMember(dynamic member)
        {
            throw new NotImplementedException();
        }
    }
}