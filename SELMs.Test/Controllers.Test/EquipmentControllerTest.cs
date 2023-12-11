using System.Web.Http.Results;

namespace SELMs.Test.Controllers.Test
{
	public class EquipmentControllerTest
	{

		private readonly ITestOutputHelper output;
		private readonly ApiEquipmentController apiEquipmentController = new();



		public EquipmentControllerTest(ITestOutputHelper output)
		{
			this.output = output;
			ApiControllerSetup.SetupController(apiEquipmentController);
		}




		#region Iteration 2


		[Theory]
		[MemberData(nameof(EquipmentControllerTestData.GetEquipmentListTestData), MemberType = typeof(EquipmentControllerTestData))]
		public async Task TestGetEquipmentList_ReturnEquipmentList(bool expectedException, Argument argument)
		{

			var actionResult = await apiEquipmentController.GetEquipmentList(argument);
			var response = await actionResult.ExecuteAsync(CancellationToken.None);
			string content = await response.Content.ReadAsStringAsync();


			if (expectedException)
			{
				Assert.IsType<BadRequestErrorMessageResult>(actionResult);
				output.WriteLine(content);
			}
			else
			{
				var list = JsonConvert.DeserializeObject<List<dynamic>>(content);

				foreach (var item in list)
					output.WriteLine(JsonConvert.SerializeObject(item));
			}



		}




		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		public async Task TestGetEquipmentById_ReturnEquipmentFound(int id)
		{
			var actionResult = await apiEquipmentController.GetEquipment(id);
			var response = await actionResult.ExecuteAsync(CancellationToken.None);
			string content = await response.Content.ReadAsStringAsync();


			if (actionResult is OkNegotiatedContentResult<EquipmentModel>)
				output.WriteLine(JsonConvert.SerializeObject(content));
			else
				output.WriteLine($"Equipment not found");
		}







		[Theory]
		[InlineData("")]
		[InlineData("E0002")]
		[InlineData("E0008")]
		public async Task TestGetEquipmentBySystemCode_ReturnEquipmentFound(string code)
		{
			DetailEquipDTO detailEquipDTO = await apiEquipmentController.GetDetailEquipment(code);

			if (detailEquipDTO.ListComponentEquips.Count > 0)
				output.WriteLine(JsonConvert.SerializeObject(detailEquipDTO));
			else
				output.WriteLine($"Equipment not found");
		}




		[Theory]
		[MemberData(nameof(EquipmentControllerTestData.CreateEquipmentTestData), MemberType = typeof(EquipmentControllerTestData))]
		public async Task TestSaveEquipmentBySystemCode_ReturnStatusCode200(EquipmentNew equipmentNew)
		{
			try
			{
				var actionResult = await apiEquipmentController.SaveEquipment(equipmentNew);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);

				Assert.Equal(200, (int)response.StatusCode);
				output.WriteLine($"Status code: {(int)response.StatusCode}");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}






		[Theory]
		[MemberData(nameof(EquipmentControllerTestData.ImportEquipmentTestData), MemberType = typeof(EquipmentControllerTestData))]
		public async Task TestImportEquipmentBySystemCode_ReturnStatusCode200(List<EquipmentDTO> equipmentDTOs)
		{
			try
			{
				var actionResult = await apiEquipmentController.ImportEquipments(equipmentDTOs);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				Thread.Sleep(3000);

				Assert.Equal(200, (int)response.StatusCode);
				output.WriteLine($"Status code: {(int)response.StatusCode}");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}




		[Theory]
		[MemberData(nameof(EquipmentControllerTestData.UpdateEquipmentTestData), MemberType = typeof(EquipmentControllerTestData))]
		public async Task TestUpdateEquipmentBySystemCode_ReturnStatusCode200(int id, EquipmentDTO equipment)
		{
			try
			{
				var actionResult = await apiEquipmentController.UpdateEquipment(id, equipment);

				Thread.Sleep(3000);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);

				Assert.Equal(200, (int)response.StatusCode);
				output.WriteLine($"Status code: {(int)response.StatusCode}");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}




		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(10)]
		public async Task TestDeleteEquipment_ReturnEquipmentFound(int id)
		{
			try
			{
				var actionResult = await apiEquipmentController.DeleteEquipment(id);
				var response = await actionResult.ExecuteAsync(CancellationToken.None);
				Thread.Sleep(2000);
				Assert.Equal(200, (int)response.StatusCode);
				output.WriteLine($"Status code: {(int)response.StatusCode}");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}
		#endregion
	}




	public static class EquipmentControllerTestData
	{

		public static IEnumerable<object[]> GetEquipmentListTestData()
		{
			//text = Equipment.serial_no , text1 = user fullname, text2 = std_code, sys_code, name

			var a = new Argument() { text = "SELT32502623-8225", text1 = "Mai Thị Ly", text2 = "E0003" };
			var b = new Argument() { text = "SELT32502623-8225", text1 = "Mai Thị Ly", text2 = "BHR4975EU" };
			var c = new Argument() { text = "SELT32502623-8225", text1 = "Mai Thị Ly", text2 = "Màn hình máy tính Xiaomi 27\" 1C BHR4975EU" };
			var d = new Argument() { text = "SELT32502623-8225", text1 = "Mai Thị Ly", text2 = "" };
			var e = new Argument() { text = "SELT32502623-8225", text1 = "Mai Thị Ly", text2 = "abc" };


			const string text = "Màn hình máy tính Xiaomi Mi Curved Gaming Monitor 34″ BHR5133GL";
			var i = new Argument() { text = "FKH8857823-349056", text1 = "A", text2 = text };
			var f = new Argument() { text = "FKH8857823-349056", text1 = "Lê Tất Đạt", text2 = "BHR5133GL" };
			var g = new Argument() { text = "FKH8857823-349056", text1 = "Đạt", text2 = "E0004" };
			var j = new Argument() { text = "FKH8857823-349056", text1 = "", text2 = "" };



			var k = new Argument() { text = "FKH8857823-349056", text1 = "Lê Tất Đạt", text2 = "Xiaomi" };
			var l = new Argument() { text = "FKH", text1 = "A", text2 = "Màn hình máy tính Xiaomi 27\" 1C BHR4975EU" };
			var m = new Argument() { text = "", text1 = "Lê Tất Đạt", text2 = "" };
			var n = new Argument() { text = "SELT32502623-8225", text1 = "Mai Thị Ly", text2 = "" };

			var o = new Argument() { text = "", text1 = "", text2 = "" };
			var p = new Argument() { text = "a", text1 = "a", text2 = "a" };



			yield return new object[] { true, null! };
			yield return new object[] { false, a };
			yield return new object[] { false, b };
			yield return new object[] { false, c };
			yield return new object[] { false, d };
			yield return new object[] { false, e };
			yield return new object[] { false, f };
			yield return new object[] { false, g };
			yield return new object[] { false, i };
			yield return new object[] { false, j };
			yield return new object[] { false, k };
			yield return new object[] { false, l };
			yield return new object[] { false, m };
			yield return new object[] { false, n };
			yield return new object[] { false, o };
			yield return new object[] { false, p };
		}



		public static IEnumerable<object[]> CreateEquipmentTestData()
		{
			EquipmentDTO e1 = new() { system_equipment_code = "E0123", equipment_name = "hoho" };
			EquipmentDTO e2 = new() { system_equipment_code = "E0456", equipment_name = "haha" };
			EquipmentDTO e3 = new() { system_equipment_code = "E0789", equipment_name = "hihi" };


			List<EquipComponentDTO> list1 = new()
			{
				new EquipComponentDTO() { equipment_name = "new new", standard_equipment_code = "E0101", system_equipment_code = "E1111" },
				new EquipComponentDTO() { equipment_name = "old", standard_equipment_code = "E0102", system_equipment_code = "E1112" }
			};

			List<EquipComponentDTO> list2 = new()
			{
				new EquipComponentDTO() { equipment_name = "very old", standard_equipment_code = "E0103", system_equipment_code = "E1113" },
				new EquipComponentDTO() { equipment_name = "null", standard_equipment_code = "E0104", system_equipment_code = "E1114" }
			};

			List<EquipComponentDTO> list3 = new()
			{
				new EquipComponentDTO() { equipment_name = "asd", standard_equipment_code = "E0105", system_equipment_code = "E1115" },
				new EquipComponentDTO() { equipment_name = "hí hí", standard_equipment_code = "E0106", system_equipment_code = "E1116" }
			};


			yield return new object[] { new EquipmentNew() { equip = e1, ListComponentEquips = list1, location_id = 1, img = null } };
			yield return new object[] { new EquipmentNew() { equip = e2, ListComponentEquips = list2, location_id = 1, img = null } };
			yield return new object[] { new EquipmentNew() { equip = e3, ListComponentEquips = list3, location_id = 1, img = null } };
		}




		public static IEnumerable<object[]> ImportEquipmentTestData()
		{
			List<EquipmentDTO> list1 = new()
			{

				new EquipmentDTO(){equipment_name="GSG9"},
				new EquipmentDTO(){equipment_name="GIGN"}
			};

			List<EquipmentDTO> list2 = new()
			{

				new EquipmentDTO(){ equipment_name="SAS"},
				new EquipmentDTO(){ equipment_name="SEAL"}
			};

			yield return new object[] { list1 };
			yield return new object[] { list2 };
		}



		public static IEnumerable<object[]> UpdateEquipmentTestData()
		{
			yield return new object[] { 0, new EquipmentDTO() { system_equipment_code = "E0010", equipment_name = "no non" } };
			yield return new object[] { 21, new EquipmentDTO() { system_equipment_code = "E0021", equipment_name = "hàng xịn" } };
			yield return new object[] { 22, new EquipmentDTO() { system_equipment_code = "E0022", equipment_name = "null null" } };
		}
	}
}
