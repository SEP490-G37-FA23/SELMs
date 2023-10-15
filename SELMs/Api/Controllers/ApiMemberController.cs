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
using System.Linq;
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
        [Route("api/Members")]
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
                Console.WriteLine($"{ex.Message} \n { ex.StackTrace}");
                return InternalServerError();
                throw;
            }
        }
        #endregion

        #region Get member by id
        [HttpGet]
        [Route("api/Member/{id}")]
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

        #region Add new member
        [HttpPost]
        [Route("api/Member/NewMember")]
        public async Task<IHttpActionResult> SaveMember(User member)
        {
            try
            {
                await service.SaveMember(member);
                return Ok();
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

        #region Update member
        [HttpPut]
        [Route("api/Member/Update/{id}")]
        public async Task<IHttpActionResult> UpdateMember(int id, [FromBody] User member)
        {
            try
            {
                await service.UpdateMember(id, member);
                return Ok();
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