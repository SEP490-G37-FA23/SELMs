using System.Collections;
using System.Reflection;

namespace SELMs.Test.Repositories.Test
{
	public class ProjectRepositoryTest
	{
		private readonly ITestOutputHelper output;
		private readonly IProjectRepository projectRepository = new ProjectRepository();

		public ProjectRepositoryTest(ITestOutputHelper output)
		{
			this.output = output;
		}




		[Fact]
		public async Task TestGetProjectList_ReturnProjectList()
		{
			var list = await projectRepository.GetProjectList();

			if (list is IList { Count: > 0 })
			{
				foreach (var item in list)
					output.WriteLine(JsonConvert.SerializeObject(item));
			}
			else
				output.WriteLine("No projects found");
		}




		[Theory]
		[MemberData(nameof(ProjectRepositoryTestData.GetProjectListByMultiConditionTestData), MemberType = typeof(ProjectRepositoryTestData))]
		public async Task TestGetProjectListByMultiCondition_ReturnListProject(Argument argument)
		{

			var list = await projectRepository.GetProjectList(argument);
			Thread.Sleep(1000);

			if (list is IList { Count: > 0 })
				foreach (var item in list)
					output.WriteLine(JsonConvert.SerializeObject(item));
			else
				output.WriteLine("Project not found");

		}




		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		public void TestProjectById_ReturnProject(int id)
		{
			var project = projectRepository.GetProject(id);

			if (project != null)
				output.WriteLine(JsonConvert.SerializeObject(project));
			else
				output.WriteLine("Project not found");
		}





		[Theory]
		[MemberData(nameof(ProjectRepositoryTestData.CreateProjectTestData), MemberType = typeof(ProjectRepositoryTestData))]
		public void TestCreateProject_ReturnProject(Project project)
		{
			try
			{
				projectRepository.SaveProject(project);

				SELMsContext sELMsContext = new();

				var p = sELMsContext.Projects.FirstOrDefault(p => p.project_name.Equals(project.project_name));

				Assert.NotNull(p);

				output.WriteLine("Project created successfully\n" + JsonConvert.SerializeObject(p));
			}
			catch (Exception ex)
			{
				Assert.IsType<ArgumentNullException>(ex);
				output.WriteLine(ex.Message);
			}

		}




		[Theory]
		[MemberData(nameof(ProjectRepositoryTestData.UpdateProjectTestData), MemberType = typeof(ProjectRepositoryTestData))]
		public void TestUpdateProject_Return(string expectedName, Project project)
		{
			try
			{
				projectRepository.UpdateProject(project);

				Thread.Sleep(2000);

				Project p = projectRepository.GetProject(project.project_id);

				Assert.Equal(expectedName, p.project_name);

				output.WriteLine("Updated successfully");
			}
			catch (TargetException ex)
			{
				Assert.IsType<TargetException>(ex);
				output.WriteLine(ex.Message);
			}
			catch (ArgumentNullException ex)
			{
				Assert.IsType<ArgumentNullException>(ex);
				output.WriteLine(ex.Message);
			}
		}







		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		public async Task TestProjectMember_ReturnMembersInProject(int projectId)
		{

			var list = await projectRepository.GetProjectMembers(projectId);

			if (list is IList { Count: > 0 })
				foreach (var item in list)
					output.WriteLine(JsonConvert.SerializeObject(item));
			else
				output.WriteLine("No member found");

		}







		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		public async Task TestProjectEquipment_ReturnEquipmentInProject(int projectId)
		{

			var list = await projectRepository.GetProjectEquipments(projectId);

			if (list is IList { Count: > 0 })
				foreach (var item in list)
					output.WriteLine(JsonConvert.SerializeObject(item));
			else
				output.WriteLine("No equipment found");
		}



		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(int.MaxValue)]
		public void TestDeleteProject_ReturnNoException(int projectId)
		{
			projectRepository.DeleteProject(projectId);
			Assert.Null(projectRepository.GetProject(projectId));
			output.WriteLine("Test case passed");
		}
	}








	public static class ProjectRepositoryTestData
	{
		public static IEnumerable<object[]> CreateProjectTestData()
		{
			yield return new object[] { null! };
			yield return new object[] { new Project() { project_name = "Ứng dụng lập trình", status = true } };
			yield return new object[] { new Project() { project_name = "Nâng cao tính thực tế của AI", status = false } };
		}


		public static IEnumerable<object[]> GetProjectListByMultiConditionTestData()
		{
			// Username is manager
			yield return new object[] { new Argument() { username = "pm", text = "very new", isadmin = false } };
			yield return new object[] { new Argument() { username = "LyMT", text = "very new", isadmin = true } };
			yield return new object[] { new Argument() { username = "LyMT", text = "", isadmin = false } };
			yield return new object[] { new Argument() { username = "DucTM4", text = "", isadmin = false } };
			yield return new object[] { new Argument() { username = "CuongNV", text = "very new" } };
		}


		public static IEnumerable<object[]> UpdateProjectTestData()
		{
			yield return new object[] { string.Empty, null! };
			yield return new object[] { "very new", new Project() { project_id = 3, project_name = "very new" } };
			yield return new object[] { "Nghiên cứu nguyên liệu hiếm", new Project() { project_id = 4, project_name = "Nghiên cứu nguyên liệu hiếm" } };
		}
	}
}
