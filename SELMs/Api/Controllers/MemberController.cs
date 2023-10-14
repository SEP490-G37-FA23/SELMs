using Dapper;
using log4net;
using SELMs.Models;
using SELMs.Models.BusinessModel;
using SELMs.Repositories;
using SELMs.Repositories.Implements;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SELMs.Api.HumanResource
{
    public class MemberController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(MemberController));

        private IMemberRepository repository = new MemberRepository();

        // GET: Api_Member
        #region Get member list
        [HttpPost]
        [Route("api/Api_Member/GetMembersList")]
        public async Task<IHttpActionResult> GetMemberList(Argument args)
        {
            try
            {
                dynamic returnedData = null;                
                returnedData = await repository.GetMemberList(args);
                return Ok(returnedData);
            }
            catch (Exception ex)
            {
                Log.Error("Error: ", ex);
                Console.WriteLine($"{ex.Message} \n { ex.StackTrace}");
                return InternalServerError();
                throw;
            }
        }

        public async Task<IHttpActionResult> GetMember(int id)
        {
            try
            {
                dynamic returnedData = null;
                returnedData = await repository.GetMember(id);
                return Ok(returnedData);
            }
            catch (Exception ex)
            {
                Log.Error("Error: ", ex);
                Console.WriteLine($"{ex.Message} \n { ex.StackTrace}");
                return InternalServerError();
                throw;
            }
        }

        #endregion
    }
}