using System.Collections;

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
			try
			{
				var list = await projectRepository.GetProjectList();

				if (list is IList { Count: > 0 })
				{
					foreach (var item in list)
						output.WriteLine(JsonConvert.SerializeObject(item));
				}
				else
					output.WriteLine("No project found");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}




		[Theory]
		[InlineData(0)]
		[InlineData(3)]
		public void TestProjectById_ReturnProject(int id)
		{
			try
			{
				var project = projectRepository.GetProject(id);

				if (project != null)
					output.WriteLine(JsonConvert.SerializeObject(project));
				else
					output.WriteLine("Project not found");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}





		[Theory]
		[MemberData(nameof(ProjectRepositoryTestData.CreateProjectTestData), MemberType = typeof(ProjectRepositoryTestData))]
		public void TestCreateProject_ReturnProject(Project project)
		{
			try
			{
				projectRepository.SaveProject(project);
				output.WriteLine("Test case passed");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
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

				output.WriteLine("Test case passed");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}







		[Theory]
		[InlineData(0)]
		[InlineData(2)]
		public async Task TestProjectMember_ReturnMemberInProject(int projectId)
		{
			try
			{
				var list = await projectRepository.GetProjectMembers(projectId);

				if (list is IList { Count: > 0 })
					foreach (var item in list)
						output.WriteLine(JsonConvert.SerializeObject(item));
				else
					output.WriteLine("No project found");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}







		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(4)]
		public async Task TestProjectEquipment_ReturnEquipmentInProject(int projectId)
		{
			try
			{
				var list = await projectRepository.GetProjectEquipments(projectId);

				if (list is IList { Count: > 0 })
					foreach (var item in list)
						output.WriteLine(JsonConvert.SerializeObject(item));
				else
					output.WriteLine("No equipment found");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}





		// not implement
		[Theory]
		[InlineData(0, "ChinhPM")]
		[InlineData(2, "ChinhPM1")]
		[InlineData(4, "DongPV")]
		public async Task TestFindActiveMember_ReturnActiveMember(int projectId, string memberCode)
		{
			try
			{
				var list = await projectRepository.FindActiveMember(projectId, memberCode);
				//Active member not found
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}
	}








	public static class ProjectRepositoryTestData
	{
		public static IEnumerable<object[]> CreateProjectTestData()
		{
			yield return new object[] { new Project() { project_name = "Ứng dụng lập trình" } };
			yield return new object[] { new Project() { project_name = "Nâng cao tính thực tế của AI" } };
		}


		public static IEnumerable<object[]> UpdateProjectTestData()
		{
			yield return new object[] { "very new", new Project() { project_id = 2, project_name = "very new" } };
			yield return new object[] { "Nghiên cứu nguyên liệu hiếm", new Project() { project_id = 4, project_name = "Nghiên cứu nguyên liệu hiếm" } };
		}
	}
}
