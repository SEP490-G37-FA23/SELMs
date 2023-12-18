using System.Collections;

namespace SELMs.Test.Repositories.Test
{
	public class LocationRepositoryTest
	{
		private readonly ITestOutputHelper output;
		private readonly ILocationRepository locationRepository = new LocationRepository();


		public LocationRepositoryTest(ITestOutputHelper output)
		{
			this.output = output;
		}



		[Fact]
		public void TestGetLocationList_ReturnList()
		{
			var list = locationRepository.GetLocationList();

			foreach (var item in list)
				output.WriteLine(JsonConvert.SerializeObject(item));
		}



		[Theory]
		[MemberData(nameof(LocationRepositoryTestData.GetLocationListByMultiConditionTestData), MemberType = typeof(LocationRepositoryTestData))]
		public async Task TestGetLocationListByMultiCondition_ReturnList(Argument argument)
		{
			var location = await locationRepository.GetLocationList(argument);

			IList list = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(location));

			if (list.Count > 0)
				foreach (var item in list)
					output.WriteLine(JsonConvert.SerializeObject(item));
			else
				output.WriteLine("Location not found");
		}





		[Theory]
		[MemberData(nameof(LocationRepositoryTestData.GetAllSubLocationListTestData), MemberType = typeof(LocationRepositoryTestData))]
		public async Task TestGetAllSubLocationList_ReturnList(Argument argument)
		{
			var location = await locationRepository.GetAllSubLocationList(argument);

			IList list = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(location));

			if (list.Count > 0)
				foreach (var item in list)
					output.WriteLine(JsonConvert.SerializeObject(item));
			else
				output.WriteLine("Location not found");
		}




		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		public void TestGetLocationById_ReturnLocation(int id)
		{
			var location = locationRepository.GetLocation(id);

			if (location != null)
				output.WriteLine(JsonConvert.SerializeObject(location));
			else
				output.WriteLine("Location not found");
		}





		[Theory]
		[MemberData(nameof(LocationRepositoryTestData.CreateLocationTestData), MemberType = typeof(LocationRepositoryTestData))]
		public void TestCreateLocation_ReturnNoException(Location location)
		{
			locationRepository.SaveLocation(location);
			output.WriteLine($"Test case passed");
		}



		[Theory]
		[MemberData(nameof(LocationRepositoryTestData.CreateLocationsTestData), MemberType = typeof(LocationRepositoryTestData))]
		public void TestCreateLocations_ReturnNoException(List<Location> locations)
		{
			locationRepository.SaveLocations(locations);
			output.WriteLine($"Test case passed");
		}





		[Theory]
		[MemberData(nameof(LocationRepositoryTestData.UpdateLocationTestData), MemberType = typeof(LocationRepositoryTestData))]
		public void TestUpdateLocations_ReturnNoException(Location location)
		{
			locationRepository.UpdateLocation(location);
			output.WriteLine($"Test case passed");
		}






		[Theory]
		[InlineData(14)]
		[InlineData(22)]
		public void TestDeleteLocationById_ReturnNoException(int id)
		{
			locationRepository.DeleteLocation(id);
			output.WriteLine("Test case passed");
		}





		[Theory]
		[InlineData(0)]
		[InlineData(2)]
		public void TestGetListSubLocation_ReturnList(int id)
		{
			var list = locationRepository.GetListSubLocation(id);

			if (list.Count > 0)
				foreach (var item in list)
					output.WriteLine(JsonConvert.SerializeObject(item));
			else
				output.WriteLine("Sub-location not found");
		}


		[Theory]
		[InlineData(2)]
		[InlineData(3)]
		public void TestGetListProjectInLocation_ReturnList(int id)
		{
			var list = locationRepository.GetListProjectInLocation(id);

			if (list.Count > 0)
				foreach (var item in list)
					output.WriteLine(JsonConvert.SerializeObject(item));
			else
				output.WriteLine("List project not found");
		}



		[Theory]
		[InlineData(0)]
		[InlineData(2)]
		public void TestGetListEquipmentInLocation_ReturnList(int id)
		{
			var list = locationRepository.GetListEquipInLocation(id);

			if (list.Count > 0)
				foreach (var item in list)
					output.WriteLine(JsonConvert.SerializeObject(item));
			else
				output.WriteLine("List equipment not found");
		}



		[Theory]
		[InlineData("E0008", -1)]
		[InlineData("E0005", 8)]
		public void TestGetEquipmentLocationHistory_ReturnList(string systemEquipmentCode, int locationId)
		{
			var history = locationRepository.GetEquipment_Location_History(systemEquipmentCode, locationId);

			if (history != null)
				output.WriteLine(JsonConvert.SerializeObject(history));
			else
				output.WriteLine("History not found");
		}




		[Theory]
		[MemberData(nameof(LocationRepositoryTestData.CreateEquipmentLocationTestData), MemberType = typeof(LocationRepositoryTestData))]
		public void TestCreateEquipmentLocationHistory_ReturnNoException(Equipment_Location_History history)
		{
			locationRepository.AddNewEquipLocationHistory(history);
			output.WriteLine("Test case passed");
		}


		[Theory]
		[MemberData(nameof(LocationRepositoryTestData.UpdateEquipmentLocationTestData), MemberType = typeof(LocationRepositoryTestData))]
		public void TestUpdateEquipmentLocationHistory_ReturnNoException(Equipment_Location_History history)
		{
			locationRepository.UpdateEquipLocationHistory(history);
			Thread.Sleep(1000);
			output.WriteLine("Update successfull");
		}
	}









	internal static class LocationRepositoryTestData
	{
		public static IEnumerable<object[]> GetLocationListByMultiConditionTestData()
		{
			// id is parent_id , text is location description, username is
			yield return new object[] { new Argument() { id = 0, text = "" } };
			yield return new object[] { new Argument() { id = 1, text = "" } };
			yield return new object[] { new Argument() { id = 2, text = "Phòng học AL-104R" } };
			yield return new object[] { new Argument() { id = 4, text = "AL-104R" } };
		}



		public static IEnumerable<object[]> GetAllSubLocationListTestData()
		{
			yield return new object[] { new Argument() { id = 1, text = "" } };
			yield return new object[] { new Argument() { id = 2, text = "Phòng học AL-104R" } };
			yield return new object[] { new Argument() { id = 4, text = "AL-104R" } };
		}



		public static IEnumerable<object[]> CreateLocationTestData()
		{
			yield return new object[] { new Location() { location_code = "AL-401L", is_active = true } };
			yield return new object[] { new Location() { location_code = "BE-406", is_active = false } };
			yield return new object[] { new Location() { location_code = "DE-210", is_active = true } };
		}



		public static IEnumerable<object[]> CreateLocationsTestData()
		{

			List<Location> locations = new()
			{
				new Location() { location_code = "AL-401R", is_active = true } ,
				new Location() { location_code = "AL-415L", is_active = false }
			};

			yield return new object[] { locations };
		}



		public static IEnumerable<object[]> UpdateLocationTestData()
		{
			yield return new object[] { new Location() { location_id = 5, location_code = "BE-215", is_active = true } };
			yield return new object[] { new Location() { location_id = 21, location_code = "BE-202", is_active = false } };
		}


		public static IEnumerable<object[]> CreateEquipmentLocationTestData()
		{
			yield return new object[] { new Equipment_Location_History() { location_id = 0, system_equipment_code = "E0055" } };
			yield return new object[] { new Equipment_Location_History() { location_id = 10, system_equipment_code = "E0021" } };
			yield return new object[] { new Equipment_Location_History() { location_id = 2, system_equipment_code = "E0009" } };
		}




		public static IEnumerable<object[]> UpdateEquipmentLocationTestData()
		{
			yield return new object[] { new Equipment_Location_History() { id = 13, system_equipment_code = "E0009", note = "kenobi" } };
			yield return new object[] { new Equipment_Location_History() { id = 15, system_equipment_code = "E0020", note = "obiwan" } };
			yield return new object[] { new Equipment_Location_History() { id = 17, system_equipment_code = "E0022", note = "skywalker" } };
		}
	}
}
