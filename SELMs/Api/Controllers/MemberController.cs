using Dapper;
using log4net;
using SELMs.Models;
using SELMs.Models.BusinessModel;
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

        private SELMsEntities db = new SELMsEntities();
        // GET: Api_Member
        #region Danh sách khách hàng
        [HttpPost]
        [Route("api/Api_Member/GetMembersList")]
        public async Task<IHttpActionResult> GetMemberList(Argument args)
        {
            try
            {
                dynamic returnedData = null;
                returnedData = db.Database.Connection.Query<dynamic>("Proc_GetMembersList", new
                {
                    username = args.username,
                }
                , commandType: CommandType.StoredProcedure).ToList();

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