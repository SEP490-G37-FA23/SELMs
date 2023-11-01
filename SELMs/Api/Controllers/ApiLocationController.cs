using AutoMapper;
using log4net;
using SELMs.App_Start;
using SELMs.Repositories.Implements;
using SELMs.Repositories;
using SELMs.Services.Implements;
using SELMs.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SELMs.Api.Controllers
{
    [RoutePrefix("api/v1")]
    public class ApiLocationController: ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ApiLocationController));

        private ILocationRepository repository = new LocationRepository();
        private IMapper mapper = MapperConfig.Initialize();

        // GET: Api_Category
        #region Get locations list
        [HttpPost]
        [Route("locations")]
        public dynamic GetLocationList()
        {
            try
            {
                dynamic returnedData = null;
                returnedData =  repository.GetLocationList();
                return Ok(returnedData);
            }
            catch (Exception ex)
            {
                Log.Error("Error: ", ex);
                Console.WriteLine($"{ex.Message} \n {ex.StackTrace}");
                return BadRequest($"{ex.Message} \n {ex.StackTrace}");
                throw;
            }
        }
        #endregion
    }
}