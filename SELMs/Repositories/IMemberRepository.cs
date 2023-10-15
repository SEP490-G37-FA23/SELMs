using System.Threading.Tasks;
using SELMs.Models.BusinessModel;

namespace SELMs.Repositories
{
    public interface IMemberRepository
    {
        dynamic GetMemberList(Argument arg);
        dynamic GetMember(int id);
        void SaveMember(dynamic member);
        void UpdateMember(dynamic member);
        dynamic DeleteMember(dynamic member);
        string GetLastMemberCode(string prefix);
    }
}
