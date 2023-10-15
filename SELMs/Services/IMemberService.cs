using SELMs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELMs.Services
{
    public interface IMemberService
    {
        Task SaveMember(User member);
        Task UpdateMember(int id, User member);
    }
}
