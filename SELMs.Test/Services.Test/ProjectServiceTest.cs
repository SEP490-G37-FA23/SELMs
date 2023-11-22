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
		[InlineData(2)]
		public async Task TestGetProjectById_ReturnProject(int id)
		{
			try
			{
				var project = await projectService.GetProject(id);

				Thread.Sleep(2000);

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
		[MemberData(nameof(ProjectServiceTestData.CreateProjectTestData), MemberType = typeof(ProjectServiceTestData))]
		public async Task TestCreateProject_ReturnProject(Project project, List<User> projectMembers, List<Equipment> projectEquipments)
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
	}





	public static class ProjectServiceTestData
	{
		public static IEnumerable<object[]> CreateProjectTestData()
		{


			//Project project, List<User> projectMembers, List<Equipment> 

			Project p1 = new() { project_name = "dunno", status = true };
			Project p2 = new() { project_name = "Lắp ráp tên lửa", status = false };
			Project p3 = new() { project_name = "Tinh chế nguyên liệu từ mặt trời", status = true };


			List<Equipment> list1 = new()
			{
				new Equipment() { equipment_id = 33, system_equipment_code = "E0032" },
				new Equipment() { equipment_id = 36, system_equipment_code = "E0034" }
			};

			List<Equipment> list3 = new()
			{
				new Equipment() { equipment_id = 24, system_equipment_code = "E0024" },
				new Equipment() { equipment_id = 27, system_equipment_code = "E0026" },
			};


			List<User> u1 = new() { new User() { user_code = "LuyenLV" }, new User() { user_code = "DucTM3" } };
			List<User> u2 = new() { new User() { user_code = "LyMT" } };


			yield return new object[] { p1, u1, list1 };
			yield return new object[] { p2, u2, null! };
			yield return new object[] { p3, null!, list3 };
		}

	}
}
