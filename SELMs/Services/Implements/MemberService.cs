using System.Threading.Tasks;
using SELMs.Models;
using SELMs.Repositories;
using SELMs.Repositories.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Configuration;
using SELMs.Api.DTOs;
using SELMs.Models.BusinessModel;

namespace SELMs.Services.Implements
{
    public class MemberService : IMemberService
    {
        private IMemberRepository repository = new MemberRepository();
        private SELMsContext db = new SELMsContext();
        private ProcessDateTime pdt = new ProcessDateTime();

        public async Task UpdateMember(int id, UserDTO member)
        {
            DateTime? dateOfBirth = null;
            DateTime? hireDate = null;
            if (member.date_of_birth != "")
            {
                dateOfBirth = pdt.Processdatetime(member.date_of_birth);
            }
            if (member.hire_date != "")
            {
                hireDate  = pdt.Processdatetime(member.hire_date);
            }
            User mem = db.Users.Where(x => x.user_id == id).FirstOrDefault();
            mem.role_code = member.role_code;
            mem.is_admin = member.is_admin;
            mem.date_of_birth = dateOfBirth;
            mem.hotline = member.hotline;
            mem.email = member.email;
            mem.gender = member.gender;
            mem.address = member.address;
            mem.avatar_img = member.avatar_img;
            mem.hire_date = hireDate;
            mem.work_term = member.work_term;
            mem.skill = member.skill;
            mem.job_description = member.job_description;
            mem.is_active = member.is_active;
            db.SaveChanges();
        }

        public async Task<User> CreateNewMember(UserDTO member)
        {
            string code = generateMemberCode(member.fullname);

            // Set attributes


            DateTime? dateOfBirth = null;
            DateTime? hireDate = null;
            if (member.date_of_birth != "")
            {
                dateOfBirth = pdt.Processdatetime(member.date_of_birth);
            }
            if (member.hire_date != "")
            {
                hireDate = pdt.Processdatetime(member.hire_date);
            }

            User mem = new User();
            mem.user_code = code;
            mem.username = code;
            mem.password = ConfigurationManager.AppSettings["DefaultPassword"];
            mem.fullname = member.fullname;
            mem.role_code = member.role_code;
            mem.is_admin = member.is_admin;
            mem.date_of_birth = dateOfBirth;
            mem.hotline = member.hotline;
            mem.email = member.email;
            mem.gender = member.gender;
            mem.address = member.address;
            mem.avatar_img = member.avatar_img;
            mem.hire_date = hireDate;
            mem.work_term = member.work_term;
            mem.skill = member.skill;
            mem.job_description = member.job_description;
            mem.is_active = member.is_active;
            db.Users.Add(mem);
            db.SaveChanges();
            return mem;
        }

        string generateMemberCode(string name)
        {
            List<string> nameParts = name.Split(' ').ToList();
            string memCode = RemoveDiacritics(nameParts.Last());
            string lastMemCode = repository.GetLastMemberCode(memCode);
            if (nameParts.Count > 1)
            {
                foreach (string item in nameParts)
                {
                    if (!item.Equals(nameParts.Last()))
                        memCode += item.First().ToString().ToUpper();
                }
            }

            if (lastMemCode.Length > 0)
            {
                string num = lastMemCode.Replace(memCode, "");
                if (num != "")
                {
                    num = (Convert.ToInt32(num) + 1).ToString();
                    memCode += num;
                }
                else
                {
                    memCode += "1";
                }
            }
            memCode = memCode.Replace("Đ", "D");
            return memCode;
        }

        string RemoveDiacritics(string s)
        {
            string normalizedString = s.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString();
        }

    }
}
