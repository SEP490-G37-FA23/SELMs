namespace SELMs.Test.Controllers.Test
{
	public class ProjectControllerTest
	{
		private readonly ITestOutputHelper output;
		private readonly ApiProjectController apiProjectController = new();

		public ProjectControllerTest(ITestOutputHelper output)
		{
			this.output = output;
			ApiControllerSetup.SetupController(apiProjectController);
		}


		#region Iteration 2

		[Fact]
		public async Task TestGetListProject_ReturnProjectList()
		{
			try
			{
				var actionResult = await apiProjectController.GetProjectList();
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				string content = await response.Content.ReadAsStringAsync();

				output.WriteLine($"Test case passed - Status code: {(int)response.StatusCode}\n{content}");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}




		[Theory]
		[MemberData(nameof(ProjectControllerTestData.SearchProjectTestData), MemberType = typeof(ProjectControllerTestData))]
		public async Task TestSearchProject_ReturnProjectFound(Argument argument)
		{
			try
			{
				var actionResult = await apiProjectController.SearchProjects(argument);
				Thread.Sleep(2000);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				output.WriteLine($"Test case passed - Status code: {(int)response.StatusCode}");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}





		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(5)]
		public async Task TestGetProjectById_ReturnProject(int id)
		{
			try
			{
				var actionResult = await apiProjectController.GetProject(id);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				string content = await response.Content.ReadAsStringAsync();

				output.WriteLine($"Test case passed - Status code: {(int)response.StatusCode}\n{content}");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}


		[Theory]
		[MemberData(nameof(ProjectControllerTestData.CreateProjectTestData), MemberType = typeof(ProjectControllerTestData))]
		public async Task TestCreateProject_ReturnStatusCode200(ProjectRequest projectRequest)
		{
			try
			{
				var actionResult = await apiProjectController.SaveProject(projectRequest);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);

				output.WriteLine($"Test case passed - Status code: {(int)response.StatusCode}");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}



		[Theory]
		[MemberData(nameof(ProjectControllerTestData.UpdateProjectTestData), MemberType = typeof(ProjectControllerTestData))]
		public async Task TestUpdateProject_ReturnStatusCode200(int id, ProjectRequest projectRequest)
		{
			try
			{
				var actionResult = await apiProjectController.UpdateProject(id, projectRequest);
				Thread.Sleep(2000);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				output.WriteLine($"Test case passed - Status code: {(int)response.StatusCode}");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}



		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		public async Task TestCancelProject_ReturnStatusCode200(int id)
		{
			try
			{
				var actionResult = await apiProjectController.CancelProject(id);
				Thread.Sleep(2000);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				output.WriteLine($"Test case passed - Status code: {(int)response.StatusCode}");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}
		#endregion
	}









	public static class ProjectControllerTestData
	{
		public static IEnumerable<object[]> CreateProjectTestData()
		{
			ProjectDTO p1 = new() { project_name = "new", status = true };
			ProjectDTO p2 = new() { project_name = "phát triển bom nguyên tử", status = false };
			ProjectDTO p3 = new() { project_name = "Nâng cấp tàu vũ trụ", status = true };


			List<EquipmentDTO> list1 = new()
			{
				new EquipmentDTO() { equipment_id = 33, system_equipment_code = "E0032" },
				new EquipmentDTO() { equipment_id = 36, system_equipment_code = "E0034" }
			};

			List<EquipmentDTO> list3 = new()
			{
				new EquipmentDTO() { equipment_id = 24, system_equipment_code = "E0024" },
				new EquipmentDTO() { equipment_id = 27, system_equipment_code = "E0026" },
			};


			List<UserDTO> u1 = new() { new UserDTO() { user_code = "ChinhPM" }, new UserDTO() { user_code = "ChinhPM1" } };
			List<UserDTO> u2 = new() { new UserDTO() { user_code = "AnhNV1" }, new UserDTO() { user_code = "DongPV" } };


			yield return new object[] { new ProjectRequest() { Project = p1, ProjectEquipments = list1, ProjectMembers = u1 } };
			yield return new object[] { new ProjectRequest() { Project = p2, ProjectEquipments = null, ProjectMembers = u2 } };
			yield return new object[] { new ProjectRequest() { Project = p3, ProjectEquipments = list3, ProjectMembers = null } };
		}




		public static IEnumerable<object[]> SearchProjectTestData()
		{
			yield return new object[] { new Argument() { username = "" } };
			yield return new object[] { new Argument() { username = "new" } };
			yield return new object[] { new Argument() { username = "b" } };
		}




		public static IEnumerable<object[]> UpdateProjectTestData()
		{
			ProjectDTO p1 = new() { project_name = "new number 2", status = false };
			ProjectDTO p2 = new() { project_name = "nâng cấp bom nguyên tử", status = true };
			ProjectDTO p3 = new() { project_name = "ko biết", status = false };


			List<EquipmentDTO> list1 = new()
			{
				new EquipmentDTO() { equipment_id = 2, system_equipment_code = "E0002" },
				new EquipmentDTO() { equipment_id = 6, system_equipment_code = "E0006" }
			};

			List<EquipmentDTO> list3 = new()
			{
				new EquipmentDTO() { equipment_id = 18, system_equipment_code = "E0009" },
				new EquipmentDTO() { equipment_id = 19, system_equipment_code = "E0010" },
			};


			List<UserDTO> u1 = new() { new UserDTO() { user_code = "DucTM" }, new UserDTO() { user_code = "ChinhPM1" } };
			List<UserDTO> u2 = new() { new UserDTO() { user_code = "AnhNV1" }, new UserDTO() { user_code = "LuatNV" } };


			ProjectRequest pr1 = new() { Project = p1, ProjectEquipments = list1, ProjectMembers = u1 };
			ProjectRequest pr2 = new() { Project = p2, ProjectEquipments = null, ProjectMembers = u2 };
			ProjectRequest pr3 = new() { Project = p3, ProjectEquipments = list3, ProjectMembers = null };


			yield return new object[] { 0, pr1 };
			yield return new object[] { 1, pr2 };
			yield return new object[] { 4, pr3 };
		}
	}
}
