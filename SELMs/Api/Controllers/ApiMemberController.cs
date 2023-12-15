using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
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

namespace SELMs.Api.HumanResource
{
	[RoutePrefix("api/v1")]
	public class ApiMemberController : ApiController
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(ApiMemberController));

		private IMemberRepository repository = new MemberRepository();
		private IMemberService service = new MemberService();
		private IMapper mapper = MapperConfig.Initialize();

		private SELMsContext db = new SELMsContext();

		// GET: Api_Member
		#region Get member list
		[HttpPost]
		[Route("members")]
		public async Task<IHttpActionResult> GetMemberList(Argument arg)
		{
			try
			{
				dynamic returnedData = null;
				returnedData = await repository.GetMemberList(arg);
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

		#region Get role list
		[HttpPost]
		[Route("members/roles")]
		public async Task<IHttpActionResult> GetRoleList()
		{
			try
			{
				dynamic returnedData = null;
				returnedData = await repository.GetRoleList();
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

		#region Get member by id
		[HttpGet]
		[Route("members/{id}")]
		public async Task<IHttpActionResult> GetMember(int id)
		{
			try
			{
				dynamic returnedData = null;
				returnedData = await repository.GetMember(id);
				return Ok(returnedData != null ? returnedData : new User());
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

		#region Add new member
		[HttpPost]
		[Route("members/new-member")]
		public async Task<IHttpActionResult> CreateNewMember(UserDTO dto)
		{
			try
			{
				User member = mapper.Map<User>(dto);
				await service.SaveMember(member);

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

		#region Update member
		[HttpPost]
		[Route("members/update/{id}")]
		public async Task<IHttpActionResult> UpdateMember(int id, [FromBody] UserDTO dto)
		{
			try
			{
				User member = mapper.Map<User>(dto);
				await service.UpdateMember(id, member);
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

		#region change-password member
		[HttpPost]
		[Route("members/change-password/{id}")]
		public async Task<IHttpActionResult> ChangePassword(int id, Argument arg)
		{
			try
			{
				User mem = db.Users.Where(x => x.user_id == id).FirstOrDefault();
				if (arg.text == mem.password && arg.text1 == arg.text2)
				{
					mem.password = arg.text1;
					db.SaveChanges();
					return Ok("Cập nhật mật khẩu thành công!");
				}
				else if (arg.text != mem.password)
				{
					return BadRequest("Nhập sai mật khẩu!!!");
				}
				else if (arg.text1 != arg.text2)
				{
					return BadRequest("Mật khẩu nhập lại không trùng khớp.");
				}
				return BadRequest();


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

		#region resign member
		[HttpPost]
		[Route("members/resign/{id}")]
		public async Task<IHttpActionResult> ResignMember(int id)
		{
			try
			{
				User mem = db.Users.Where(x => x.user_id == id).FirstOrDefault();
				mem.resignation_date = DateTime.Now;
				mem.is_active = false;
				db.SaveChanges();
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