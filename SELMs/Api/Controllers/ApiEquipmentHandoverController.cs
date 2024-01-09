using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
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

namespace SELMs.Api.Controllers
{
	[RoutePrefix("api/v1")]
	public class ApiEquipmentHandoverController : ApiController
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(ApiEquipmentHandoverController));

		private IEquipmentHandoverFormRepository repository = new EquipmentHandoverFormRepository();
		private IEquipmentHandoverService service = new EquipmentHandoverService();
		private IMapper mapper = MapperConfig.Initialize();

		#region Get application
		[HttpPost]
		[Route("equipment-handover/forms")]
		public async Task<IHttpActionResult> GetEHFList(Argument arg)
		{
			try
			{
				dynamic returnedData = null;
				returnedData = repository.GetApplicationList(arg);
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

		#region Get application by id
		[HttpGet]
		[Route("equipment-handover-form/{id}")]
		public async Task<IHttpActionResult> GetApplication(int id)
		{
			try
			{
				dynamic returnedData = await service.GetApplication(id);
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




		[HttpGet]
		[Route("equipment-handover-form/details/list")]
		public async Task<IHttpActionResult> get()
		{



			return Ok(await new SELMsContext().Equipment_Handover_Form.ToListAsync());


		}


		#region Get application by id 2
		[HttpGet]
		[Route("equipment-handover-form/details/{formCode}")]
		public async Task<IHttpActionResult> GetApplicationDetail(string formCode)
		{
			try
			{
				var originalFormDetail = await repository.GetFormDetail(formCode);
				EquipmentHandoverFormDTO2 formDetail = mapper.Map<EquipmentHandoverFormDTO2>(originalFormDetail);

				List<Equipment_Handover_Form_Detail> detailList = repository.GetApplicationDetailList(formDetail.form_code);
				formDetail.Equipment_Handover_Form_Detail = mapper.Map<List<EquipmentHandoverFormDetailDTO>>(detailList);

				return Ok(formDetail);
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

		#region Add new application
		[HttpPost]
		[Route("equipment-handover-form/new-form")]
		public async Task<IHttpActionResult> SaveApplication([FromBody] EquipmentHandoverRequest applicationRequest)
		{
			try
			{
				Equipment_Handover_Form application = mapper.Map<Equipment_Handover_Form>(applicationRequest.application);
				List<Equipment_Handover_Form_Detail> details = mapper.Map<List<Equipment_Handover_Form_Detail>>(applicationRequest.application_details);
				service.SaveApplication(application, details);
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

		#region Update application
		[HttpPut]
		[Route("equipment-handover_form/update/{id}")]
		public async Task<IHttpActionResult> UpdateApplication(int id, [FromBody] EquipmentHandoverRequest applicationRequest)
		{
			try
			{
				Equipment_Handover_Form application = mapper.Map<Equipment_Handover_Form>(applicationRequest.application);
				List<Equipment_Handover_Form_Detail> details = mapper.Map<List<Equipment_Handover_Form_Detail>>(applicationRequest.application_details);
				service.UpdateApplication(id, application, details);
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

		#region Attach file to application
		[HttpPost]
		[Route("equipment-handover/finish-file/")]
		public async Task<IHttpActionResult> AttachFile()
		{
			try
			{
				var result = new List<dynamic>();
				var applicationId = HttpContext.Current.Request.Params["application_id"];
				var files = HttpContext.Current.Request.Files.GetMultiple("attachments");
				foreach (var file in files)
				{
					var item = await service.AddAttachment(Convert.ToInt32(applicationId), file);
					result.Add(item);
				}
				return Ok(result);
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

		#region Remove Attachment
		[HttpPost]
		[Route("equipment-handover/remove-attachment")]
		public async Task<IHttpActionResult> AttachFile(int application_id, int attach_id)
		{
			try
			{
				service.DeleteAttachment(application_id, attach_id);
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

		#region Confirm application
		[HttpPost]
		[Route("equipment-handover/confirm/{id}")]
		public async Task<IHttpActionResult> ConfirmApplication(int id, [FromBody] UserDTO member)
		{
			try
			{
				User user = mapper.Map<User>(member);
				dynamic result = service.ConfirmApplication(id, user);
				return Ok(result);
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

		#region Cancel application
		[HttpPost]
		[Route("equipment-handover/cancel/{id}")]
		public async Task<IHttpActionResult> CancelApplication(int id)
		{
			try
			{
				service.CancelApplication(id);
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
