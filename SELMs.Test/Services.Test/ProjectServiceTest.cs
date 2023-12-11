namespace SELMs.Test.Services.Test
{
	public class ProjectServiceTest
	{
		private readonly ITestOutputHelper output;
		private readonly IProjectService projectService = new ProjectService();

		public ProjectServiceTest(ITestOutputHelper output)
		{
			this.output = output;
		}



		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		public async Task TestGetProjectById_ReturnProject(int id)
		{

			ProjectModel project = await projectService.GetProject(id);

			if (project.Project != null)
				output.WriteLine(JsonConvert.SerializeObject(project));

			else
				output.WriteLine("Project not found");
		}








		[Theory]
		[MemberData(nameof(ProjectServiceTestData.CreateProjectTestData), MemberType = typeof(ProjectServiceTestData))]
		public async Task TestCreateProject_ReturnNothing(Project project, List<User> projectMembers, List<Equipment> projectEquipments)
		{
			try
			{
				await projectService.SaveProject(project, projectMembers, projectEquipments);
				output.WriteLine("Test case passed");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}





		[Theory]
		[MemberData(nameof(ProjectServiceTestData.UpdateProjectTestData), MemberType = typeof(ProjectServiceTestData))]
		public async Task TestUpdateProject_ReturnNothing(int id, Project project, List<User> projectMembers, List<Equipment> projectEquipments)
		{
			try
			{
				await projectService.UpdateProject(id, project, projectMembers, projectEquipments);
				Thread.Sleep(1500);
				output.WriteLine("Test case passed");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}
	}





	public static class ProjectServiceTestData
	{
		public static IEnumerable<object[]> CreateProjectTestData()
		{
			//int id, Project project, List<User> projectMembers, List< Equipment > projectEquipments
			Project p1 = new() { project_name = "dunno", status = true };
			Project p2 = new() { project_name = "Lắp ráp tên lửa", status = false };
			Project p3 = new() { project_name = "Tinh chế nguyên liệu từ mặt trời", status = true };


			List<Equipment> list1 = new()
			{
				new Equipment() { equipment_id = 33, system_equipment_code = "E0032" },
				new Equipment() { equipment_id = 36, system_equipment_code = "E0034" }
			};

			List<Equipment> list2 = new()
			{
				new Equipment() { equipment_id = 6, system_equipment_code = "E0006" },
				new Equipment() { equipment_id = 8, system_equipment_code = "E0008" }
			};

			List<Equipment> list3 = new()
			{
				new Equipment() { equipment_id = 24, system_equipment_code = "E0024" },
				new Equipment() { equipment_id = 27, system_equipment_code = "E0026" },
			};


			List<User> u1 = new() { new User() { user_code = "LuyenLV" }, new User() { user_code = "DucTM3" } };
			List<User> u2 = new() { new User() { user_code = "LyMT" } };
			List<User> u3 = new() { new User() { user_code = "DucTM4" } };


			yield return new object[] { p1, u1, list1 };
			yield return new object[] { p2, u2, list2 };
			yield return new object[] { p3, u3, list3 };
		}







		public static IEnumerable<object[]> UpdateProjectTestData()
		{
			Project p1 = new() { project_name = "Kích nổ bom" };
			Project p2 = new() { project_name = "Dựng base" };


			List<Equipment> list1 = new()
			{
				new Equipment() { equipment_id = 7, system_equipment_code = "E0007" },
				new Equipment() { equipment_id = 8, system_equipment_code = "E0008" }
			};

			List<Equipment> list2 = new()
			{
				new Equipment() { equipment_id = 27, system_equipment_code = "E0026" },
				new Equipment() { equipment_id = 28, system_equipment_code = "E0026" }
			};


			List<User> u1 = new() { new User() { user_code = "LuyenLV" }, new User() { user_code = "DucTM3" } };
			List<User> u2 = new() { new User() { user_code = "LyMT" } };


			yield return new object[] { 2, p1, u1, list1 };
			yield return new object[] { 10, p2, u2, list2 };
		}
	}
}
