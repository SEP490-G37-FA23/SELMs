using SELMs.Api.DTOs;
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
        Task<User> CreateNewMember(UserDTO member);
        Task UpdateMember(int id, UserDTO member);
    }
}
