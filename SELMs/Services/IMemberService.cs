using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELMs.Services
{
    public interface IMemberService
    {
        void SaveMember(dynamic member);
        void UpdateMember(int id, dynamic member);
    }
}
