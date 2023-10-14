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
        dynamic GetMember(int id);
        void SaveMember(dynamic member);
        void UpdateMember(dynamic member);
        dynamic DeleteMember(dynamic member);
    }
}
