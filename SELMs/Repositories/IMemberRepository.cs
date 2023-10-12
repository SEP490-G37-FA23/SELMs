using SELMs.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELMs.Repositories
{
    public interface IMemberRepository
    {
        dynamic GetMemberList(Argument arg);
        dynamic GetMember();
        dynamic SaveMember(dynamic member);
        dynamic UpdateMember(dynamic member);
        dynamic DeleteMember(dynamic member);
    }
}
