using System.Threading.Tasks;
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

            //Generate member code
            List<string> nameParts = mem.fullname.Split(' ').ToList();
            string memCode = nameParts.Last();
            string lastMemCode = repository.GetLastMemberCode(memCode);
            foreach (string item in nameParts)
            {
                if (item.Equals(nameParts.Last()))
                    memCode += item.First().ToString().ToUpper();
            }
            if (lastMemCode != null)
            {
                string num = lastMemCode.Replace(memCode, "");
                if (num.Count() > 0)
                {
                    num = (Convert.ToInt32(lastMemCode) + 1).ToString();
                    memCode += num;
                }
                else
                {
                    memCode += "1";
                }
            }

            // Set attributes
            mem.hire_date = DateTime.Now;
            mem.is_admin = false;
            mem.active = true;
            mem.member_code = memCode;

            repository.SaveMember(mem);
        }
    }
}
