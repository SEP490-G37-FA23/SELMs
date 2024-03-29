﻿using System;
using System.Collections.Generic;
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

namespace SELMs.Api.Controllers
{
	[RoutePrefix("api/v1")]
	public class ApiProjectController : ApiController
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(ApiProjectController));

		private IProjectRepository repository = new ProjectRepository();
		private IProjectService service = new ProjectService();
		private IMapper mapper = MapperConfig.Initialize();

		// GET: Api_Project
		#region Get project list
		[HttpGet]
		[Route("projects")]
		public async Task<IHttpActionResult> GetProjectList()
		{
			try
			{
				dynamic returnedData = null;
				returnedData = await repository.GetProjectList();
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

		#region Search Project
		[HttpPost]
		[Route("projects/search")]
		public async Task<IHttpActionResult> SearchProjects(Argument args)
		{
			try
			{
				dynamic returnedData = null;
				returnedData = await repository.GetProjectList(args);
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

		#region Search Project
		[HttpPost]
		[Route("projects/doing")]
		public async Task<IHttpActionResult> GetDoingProjectsList(Argument args)
		{
			try
			{
				dynamic returnedData = null;
				returnedData = await repository.GetDoingProjectList(args);
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

		#region Get project by id
		[HttpGet]
		[Route("projects/{id}")]
		public async Task<IHttpActionResult> GetProject(int id)
		{
			try
			{
				dynamic returnedData = null;
				returnedData = await service.GetProject(id);
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

		#region Add new project
		[HttpPost]
		[Route("projects/new-project")]
		public async Task<IHttpActionResult> SaveProject(ProjectRequest projectRequest)
		{
			try
			{
				projectRequest.Project.create_date = DateTime.Now;
				Project project = mapper.Map<Project>(projectRequest.Project);
				List<string> projectMembers = projectRequest.ProjectMembers;

				List<string> projectEquipments = projectRequest.ProjectEquipments;
				await service.SaveProject(project, projectMembers, projectEquipments);
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

		#region Update project
		[HttpPost]
		[Route("projects/update/{id}")]
		public async Task<IHttpActionResult> UpdateProject(int id, [FromBody] ProjectRequest projectRequest)
		{
			try
			{
				Project project = mapper.Map<Project>(projectRequest.Project);
				List<string> projectMembers = projectRequest.ProjectMembers;
				List<string> projectEquipments = projectRequest.ProjectEquipments;
				service.UpdateProject(id, project, projectMembers, projectEquipments);
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

		#region Cancel project
		[HttpPost]
		[Route("projects/cancel/{id}")]
		public async Task<IHttpActionResult> CancelProject(int id)
		{
			try
			{
				service.CancelProject(id);
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

		#region Delete Project
		[HttpPost]
		[Route("projects/delete/{id}")]
		public async Task<IHttpActionResult> DeleteProjects(int id)
		{
			try
			{
				repository.DeleteProject(id);
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


		#region Remove Project Member
		[HttpPost]
		[Route("projects/{projectId}/remove-member/{userCode}")]
		public async Task<IHttpActionResult> RemoveProjectMember(int projectId, string userCode)
		{
			try
			{
				repository.RemoveProjectMember(projectId,userCode);
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

		//#region Get detail project
		//[HttpGet]
		//[Route("projects/detail/{id}")]
		//public async Task<DetailProjectDTO> GetDetailProject(int id)
		//{
		//	DetailProjectDTO returnedData = new DetailProjectDTO();
		//	returnedData.project = await repository.GetDetailProject(id);
		//	returnedData.ListMemberInProject = repository.GetListMemberInProject(id);
		//	returnedData.ListEquipmentInProject = repository.GetListEquipmentInProject(id);
		//	return returnedData;


		//}
		//#endregion
	}
}
