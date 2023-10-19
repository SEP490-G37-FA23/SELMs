using Dapper;
using log4net;
using SELMs.Models;
using SELMs.Models.BusinessModel;
using SELMs.Repositories;
using SELMs.Repositories.Implements;
using SELMs.Services;
using SELMs.Services.Implements;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SELMs.Api.HumanResource
{
    public class ApiMemberController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ApiMemberController));

        private IMemberRepository repository = new MemberRepository();
        private IMemberService service = new MemberService();

        // GET: Api_Member
        #region Get member list
        [HttpPost]
        [Route("api/Members/List")]
        public dynamic GetMemberList(Argument args)
        {
            try
            {
                dynamic returnedData = null;
                returnedData = repository.GetMemberList(args);
                return Ok(returnedData);
            }
            catch (Exception ex)
            {
                Log.Error("Error: ", ex);
                Console.WriteLine($"{ex.Message} \n {ex.StackTrace}");
                return InternalServerError();
                throw;
            }
        }
        #endregion

        #region Search Member
        [HttpPost]
        [Route("api/Members/Search")]
        public async Task<IHttpActionResult> SearchMembers(Argument args)
        {
            try
            {
                dynamic returnedData = null;
                returnedData = await repository.SearchMembers(args);
                return Ok(returnedData);
            }
            catch (Exception ex)
            {
                Log.Error("Error: ", ex);
                Console.WriteLine($"{ex.Message} \n {ex.StackTrace}");
                return InternalServerError();
                throw;
            }
        }
        #endregion

        #region Get member by id
        [HttpGet]
        [Route("api/Member/{id}")]
        public dynamic GetMember(int id)
        {
            try
            {
                dynamic returnedData = null;
                returnedData = repository.GetMember(id);
                return Ok(returnedData);
            }
            catch (Exception ex)
            {
                Log.Error("Error: ", ex);
                Console.WriteLine($"{ex.Message} \n {ex.StackTrace}");
                return InternalServerError();
                throw;
            }
        }
        #endregion

        #region Add new member
        static string ConvertToAbbreviation(string input)
        {
            string[] words = input.Split(' ');

            if (words.Length < 2)
            {
                // Not enough words to create an abbreviation
                return input;
            }

            StringBuilder abbreviation = new StringBuilder();

            // Get the last word without diacritics
            string lastName = words[words.Length - 1];
            string lastNameWithoutDiacritics = RemoveDiacritics(lastName);
            abbreviation.Append(lastNameWithoutDiacritics);

            // Get the first letters of the initial words
            for (int i = 0; i < words.Length - 1; i++)
            {
                if (words[i].Length > 0)
                {
                    char firstLetter = words[i][0];
                    abbreviation.Append(firstLetter);
                }
            }



            return abbreviation.ToString();
        }

        static string RemoveDiacritics(string s)
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


        [HttpPost]
        [Route("api/Member/NewMember")]
        public async Task<IHttpActionResult> SaveMember(dynamic member)
        {
            try
            {
                string result = ConvertToAbbreviation(member.fullname);
                result = result.Replace("Đ", "D");
                dynamic returnedData = null;
                returnedData = await service.SaveMember(member);
                return Ok(returnedData);
            }
            catch (Exception ex)
            {
                Log.Error("Error: ", ex);
                Console.WriteLine($"{ex.Message} \n {ex.StackTrace}");
                return InternalServerError();
                throw;
            }
        }
        #endregion

        #region Update member
        [HttpPut]
        [Route("api/Member/NewMember")]
        public async Task<IHttpActionResult> SaveMember(int id, dynamic member)
        {
            try
            {
                dynamic returnedData = null;
                returnedData = await service.UpdateMember(id, member);
                return Ok(returnedData);
            }
            catch (Exception ex)
            {
                Log.Error("Error: ", ex);
                Console.WriteLine($"{ex.Message} \n {ex.StackTrace}");
                return InternalServerError();
                throw;
            }
        }
        #endregion
    }
}