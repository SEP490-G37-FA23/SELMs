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

namespace SELMs.Services.Implements
{
    public class MemberService : IMemberService
    {
        private IMemberRepository repository = new MemberRepository();
        public async Task SaveMember(User member)
        {
            User mem = member;

            //Generate member code
            string code = generateMemberCode(mem.fullname);

            // Set attributes
            mem.hire_date = DateTime.Now;
            mem.is_admin = false;
            mem.is_active = true;
            mem.user_code = code;
            mem.username = code;
            mem.password = ConfigurationManager.AppSettings["DefaultPassword"];

            repository.SaveMember(mem);
        }

        public async Task UpdateMember(int id, User member)
        {
            if (await repository.GetMember(id) != null)
            {
                member.user_id = id;
                repository.UpdateMember(member);
            }
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
                if (num.Count() > 0)
                {
                    num = (Convert.ToInt32(num) + 1).ToString();
                    memCode += num;
                }
                else
                {
                    memCode += "1";
                }
            }
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
