using AutoMapper;
using Dapper;
using log4net;
using SELMs.Api.DTOs;
using SELMs.App_Start;
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
        private IMapper mapper = MapperConfig.Initialize();

        // GET: Api_Member
        #region Get member list
        [HttpPost]
        [Route("api/Members")]
        public async Task<IHttpActionResult> GetMemberList()
        {
            try
            {
                dynamic returnedData = null;                
                returnedData = await repository.GetMemberList();
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
        public async Task<IHttpActionResult> SaveMember(UserDTO member)
        {
            try
            {
                User user = mapper.Map<User>(member);
                await service.SaveMember(user);
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
        public async Task<IHttpActionResult> UpdateMember(int id, [FromBody] UserDTO member)
        {
            try
            {
                User mem = mapper.Map<User>(member);
                await service.UpdateMember(id, mem);
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