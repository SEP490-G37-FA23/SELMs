using System.Threading.Tasks;
using SELMs.Models;
using SELMs.Models.BusinessModel;

namespace SELMs.Repositories
{
    public interface IMemberRepository
    {
        dynamic GetMemberList();
        dynamic SearchMembers(Argument arg);
        dynamic GetMember(int id);
        void SaveMember(User member);
        void UpdateMember(User member);
        dynamic DeleteMember(User member);
        string GetLastMemberCode(string prefix);
    }
}
