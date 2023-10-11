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

        public dynamic deleteMember(dynamic member)
        {
            throw new NotImplementedException();
        }

        public dynamic getMember()
        {
            throw new NotImplementedException();
        }

        public dynamic getMemberList(Argument args)
        {
            dynamic members = null;
            members = db.Database.Connection.Query<dynamic>("Proc_GetMembersList", new
            {
                username = args.username,
            }
                , commandType: CommandType.StoredProcedure).ToList();
            return members;
        }

        public dynamic saveMember(dynamic member)
        {

            throw new NotImplementedException();
        }

        public dynamic updateMember(dynamic member)
        {
            throw new NotImplementedException();
        }
    }
}