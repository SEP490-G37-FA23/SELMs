using SELMs.Models;
using SELMs.Repositories;
using SELMs.Repositories.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELMs.Services.Implements
{
    public class MemberService : IMemberService
    {
        private IMemberRepository repository = new MemberRepository();
        public void SaveMember(dynamic member)
        {
            User mem = (User)member;
            mem.hire_date = DateTime.Now;
            mem.is_admin = false;
            mem.active = true;
            repository.SaveMember(mem);
        }
    }
}
