using System.Threading.Tasks;
using SELMs.Models;
using SELMs.Models.BusinessModel;

namespace SELMs.Repositories
{
    public interface IMemberRepository
    {
        dynamic GetMemberList(Argument arg);
        dynamic GetRoleList();
        dynamic GetMember(int id);
        dynamic SaveMember(User member);
        void UpdateMember(User member);
        dynamic DeleteMember(User member);
        string GetLastMemberCode(string prefix);
        Task SendGmailAllocationAccountAsync(User mem);
    }
}
