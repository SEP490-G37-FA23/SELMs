using AutoMapper;
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
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SELMs.Api.Controllers
{
    [RoutePrefix("api/v1")]
    public class ApiCategoryController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ApiCategoryController));

        private ICategoryRepository repository = new CategoryRepository();
        private ICategoryService service = new CategoryService();
        private IMapper mapper = MapperConfig.Initialize();

        // GET: Api_Category
        #region Get category list
        [HttpPost]
        [Route("categories")]
        public async Task<IHttpActionResult> GetCategoryList()
        {
            try
            {
                dynamic returnedData = null;                
                returnedData = await repository.GetCategoryList();
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

        #region Search Category
        [HttpPost]
        [Route("categories-list")]
        public async Task<IHttpActionResult> GetCategories(Argument args)
        {
            try
            {
                dynamic returnedData = null;
                returnedData = await repository.SearchCategories(args);
                return Ok(returnedData);
            }
            catch (Exception ex)
            {
                Log.Error("Error: ", ex);
                Console.WriteLine($"{ex.Message} \n { ex.StackTrace}");
                return BadRequest($"{ex.Message} \n {ex.StackTrace}");
                throw;
            }
        }
        #endregion

        #region Get category by id
        [HttpGet]
        [Route("categories/{id}")]
        public async Task<IHttpActionResult> GetCategory(int id)
        {
            try
            {
                dynamic returnedData = null;
                returnedData = repository.GetCategory(id);
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

        #region Add new category
        [HttpPost]
        [Route("api/categories/new-category")]
        public async Task<IHttpActionResult> SaveCategory([FromBody]CategoryRequest categoryRequest)
        {
            try
            {
                Category category = mapper.Map<Category>(categoryRequest.category);
                List<Equipment> equipments = mapper.Map<List<Equipment>>(categoryRequest.equipments);
                service.SaveCategory(category,equipments);
                return Ok();
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

        #region Update category
        [HttpPut]
        [Route("categories/{id}")]
        public async Task<IHttpActionResult> UpdateCategory(int id, [FromBody] CategoryDTO category)
        {
            try
            {
                Category mem = mapper.Map<Category>(category);
                service.UpdateCategory(id, mem);
                return Ok();
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
