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
        Task SaveMember(dynamic member);
        Task UpdateMember(int id, dynamic member);
    }
}
