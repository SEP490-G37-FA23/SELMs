using System.Collections;
using System.Data.Entity;
using System.Web.Http.Results;
using Newtonsoft.Json.Linq;

namespace SELMs.Test.Controllers.Test
{
	public class LocationControllerTest
	{
		private readonly ITestOutputHelper output;
		private readonly SELMsContext sELMsContext = new();
		private readonly ApiLocationController apiLocationController = new();

		public LocationControllerTest(ITestOutputHelper output)
		{
			this.output = output;
			ApiControllerSetup.SetupController(apiLocationController);
		}






		#region Iteration 3
		[Fact]
		public async Task TestGetLocationList_ReturnList()
		{

			var actionResult = await apiLocationController.GetLocationList();
			var response = await actionResult.ExecuteAsync(CancellationToken.None);
			string content = await response.Content.ReadAsStringAsync();

			IList list = JsonConvert.DeserializeObject<List<dynamic>>(content);

			foreach (var item in list)
				output.WriteLine(JsonConvert.SerializeObject(item));
		}




		[Theory]
		[MemberData(nameof(LocationControllerTestData.GetLocationListByMultiConditionTestData), MemberType = typeof(LocationControllerTestData))]
		public async Task TestGetLocationListByMultiCondition_ReturnList(bool expectException, Argument argument)
		{

			var actionResult = await apiLocationController.GetLocationList(argument);
			Thread.Sleep(1000);
			var response = await actionResult.ExecuteAsync(CancellationToken.None);
			string content = await response.Content.ReadAsStringAsync();


			if (!expectException)
			{
				Assert.Equal(200, (int)response.StatusCode);
				var list = JsonConvert.DeserializeObject<List<dynamic>>(content);

				if (list.Count > 0)
					foreach (var item in list)
						output.WriteLine(JsonConvert.SerializeObject(item));
				else
					output.WriteLine("Location not found");
			}
			else
			{
				Assert.Equal(400, (int)response.StatusCode);
				Assert.IsType<BadRequestErrorMessageResult>(actionResult);
				output.WriteLine(content);
			}
		}





		[Theory]
		[MemberData(nameof(LocationControllerTestData.GetAllSubLocationListTestData), MemberType = typeof(LocationControllerTestData))]
		public async Task TestGetAllSubLocationList_ReturnList(bool exception, Argument argument)
		{

			var actionResult = await apiLocationController.GetAllSubLocationList(argument);
			Thread.Sleep(1000);
			var response = await actionResult.ExecuteAsync(CancellationToken.None);
			string content = await response.Content.ReadAsStringAsync();


			if (exception)
			{
				Assert.Equal(400, (int)response.StatusCode);
				Assert.IsType<BadRequestErrorMessageResult>(actionResult);
				output.WriteLine(content);
			}
			else
			{
				Assert.Equal(200, (int)response.StatusCode);
				output.WriteLine(content);
			}

		}








		[Theory]
		[MemberData(nameof(LocationControllerTestData.CreateLocationTestData), MemberType = typeof(LocationControllerTestData))]
		public async Task TestCreateLocation_ReturnStatusCode200(bool expectException, LocationRequest locationRequest)
		{

			var actionResult = await apiLocationController.SaveLocation(locationRequest);
			Thread.Sleep(2000);
			var response = await actionResult.ExecuteAsync(CancellationToken.None);


			if (expectException)
			{
				string content = await response.Content.ReadAsStringAsync();
				Assert.Equal(400, (int)response.StatusCode);
				output.WriteLine(content);
			}
			else
			{
				Assert.Equal(200, (int)response.StatusCode);
				output.WriteLine("Add new success");
			}
		}





		[Theory]
		[MemberData(nameof(LocationControllerTestData.UpdateLocationTestData), MemberType = typeof(LocationControllerTestData))]
		public async Task TestUpdateLocation_ReturnStatusCode200(int id, LocationDTO locationDTO)
		{

			var actionResult = await apiLocationController.UpdateLocation(id, locationDTO);
			Thread.Sleep(2000);
			var response = await actionResult.ExecuteAsync(CancellationToken.None);

			Assert.Equal(200, (int)response.StatusCode);


			var actionResult1 = await apiLocationController.GetLocation(id);
			var response1 = await actionResult1.ExecuteAsync(CancellationToken.None);
			string content1 = await response1.Content.ReadAsStringAsync();


			if (content1.Equals("null"))
			{
				LocationDTO l = JsonConvert.DeserializeObject<LocationDTO>(content1);

				Assert.Equal(locationDTO.location_code, l.location_code);
				Assert.Equal(locationDTO.is_active, l.is_active);

				output.WriteLine("Update sucessfull");
			}
			else
				output.WriteLine("Location not found");
		}






		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(9)]
		public void TestGetDetailLocation_ReturnStatusCode200(int id)
		{
			var location = apiLocationController.GetDetailLocation(id);

			if (location.location_info == null)
				output.WriteLine("Location not found");
			else
			{
				JToken jsonToken = JToken.Parse(JsonConvert.SerializeObject(location));
				output.WriteLine(jsonToken.ToString(Formatting.Indented));
			}
		}





		[Theory]
		[MemberData(nameof(LocationControllerTestData.CreateEquipLocationHistoryTestData), MemberType = typeof(LocationControllerTestData))]
		public async Task TestCreateEquipLocationHistory_ReturnStatusCode200(bool expectException, EquipLocationHistoryDTO dto)
		{

			var actionResult = await apiLocationController.SaveEquipLocationHistory(dto);
			Thread.Sleep(2000);
			var response = await actionResult.ExecuteAsync(CancellationToken.None);


			if (expectException)
			{
				string content = await response.Content.ReadAsStringAsync();
				Assert.Equal(400, (int)response.StatusCode);
				output.WriteLine(content);
			}
			else
			{
				Assert.Equal(200, (int)response.StatusCode);
				output.WriteLine("Add new success");
			}
		}





		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(44)]
		public async Task TestGetLocationById_ReturnLocation(int id)
		{
			var actionResult = await apiLocationController.GetLocation(id);
			var response = await actionResult.ExecuteAsync(CancellationToken.None);
			string content = await response.Content.ReadAsStringAsync();

			if ((int)response.StatusCode == 200)
				output.WriteLine(content);
			else
				output.WriteLine("Location not found");
		}







		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		public async Task TestDeleteLocation_ReturnStatusCode200(int id)
		{
			var actionResult = await apiLocationController.DeleteLocations(id);
			Thread.Sleep(1000);
			var response = await actionResult.ExecuteAsync(CancellationToken.None);

			Assert.Equal(200, (int)response.StatusCode);


			var location = await sELMsContext.Locations.FirstOrDefaultAsync(l => l.location_id == id);

			if (location == null)
				output.WriteLine("Delete successfull");
		}
		#endregion
	}







	public static class LocationControllerTestData
	{
		public static IEnumerable<object[]> GetLocationListByMultiConditionTestData()
		{
			// id is parent_id , text is location description
			yield return new object[] { true, null! };
			yield return new object[] { false, new Argument() { id = 1, text = "" } };
			yield return new object[] { false, new Argument() { id = 2, text = "Tủ" } };
			yield return new object[] { false, new Argument() { id = 4, text = "Ngăn cột 1, Tủ 102, Phòng học AL-104R" } };
		}



		public static IEnumerable<object[]> GetAllSubLocationListTestData()
		{
			yield return new object[] { true, null! };
			yield return new object[] { false, new Argument() { id = 1, text = "" } };
			yield return new object[] { false, new Argument() { id = 2, text = "Phòng học AL-104R" } };
			yield return new object[] { false, new Argument() { id = 4, text = "AL-104R" } };
		}



		public static IEnumerable<object[]> CreateLocationTestData()
		{
			LocationDTO l1 = new() { location_code = "AL-213R", is_active = true };
			LocationDTO l2 = new() { location_code = "AL-214R", is_active = true };
			LocationDTO l3 = new() { location_code = "AL-215R", is_active = false };


			List<LocationDTO> list1 = new()
			{
				new LocationDTO() { location_code = "BE-110", is_active = false },
				new LocationDTO() { location_code = "BE-210", is_active = true }
			};

			List<LocationDTO> list2 = new() { new LocationDTO() { location_code = "BE-111", is_active = true } };

			List<LocationDTO> list3 = new() { new LocationDTO() { location_code = "BE-113", is_active = false } };


			yield return new object[] { true, null! };
			yield return new object[] { false, new LocationRequest() { Location = l1, SubLocations = list1 } };
			yield return new object[] { false, new LocationRequest() { Location = l2, SubLocations = list2 } };
			yield return new object[] { false, new LocationRequest() { Location = l3, SubLocations = list3 } };
		}




		public static IEnumerable<object[]> UpdateLocationTestData()
		{
			LocationDTO l1 = new() { location_code = "", is_active = true };
			LocationDTO l2 = new() { location_code = "DE-318", is_active = true };

			yield return new object[] { 1, l1 };
			yield return new object[] { 20, l2 };
		}






		public static IEnumerable<object[]> CreateEquipLocationHistoryTestData()
		{
			List<Equipment_Location_History> list1 = new()
			{
				new Equipment_Location_History() { system_equipment_code = "E0055", location_id = 1 }
			};

			List<Equipment_Location_History> list2 = new()
			{
				new Equipment_Location_History() { system_equipment_code = "E0062", location_id = 4 }
			};

			List<Equipment_Location_History> list3 = new()
			{
				new Equipment_Location_History() { system_equipment_code = "E0052", location_id = 4 },
				new Equipment_Location_History() { system_equipment_code = "E0044", location_id = 5 }
			};

			yield return new object[] { true, null! };
			yield return new object[] { false, new EquipLocationHistoryDTO() { ListEquipLocationHistory = list1 } };
			yield return new object[] { false, new EquipLocationHistoryDTO() { ListEquipLocationHistory = list2 } };
			yield return new object[] { false, new EquipLocationHistoryDTO() { ListEquipLocationHistory = list3 } };
		}
	}
}
