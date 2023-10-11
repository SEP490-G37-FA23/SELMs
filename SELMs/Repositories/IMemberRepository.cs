using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELMs.Repositories
{
    public interface IMemberRepository
    {
        dynamic getMemberList();
        dynamic getMember();
        dynamic saveMember(dynamic member);
        dynamic updateMember(dynamic member);
        dynamic deleteMember(dynamic member);
    }
}
