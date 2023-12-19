using System.Data.Entity;

namespace SELMs.Test.Services.Test
{
	public class LocationServiceTest
	{
		private readonly ITestOutputHelper output;
		private readonly ILocationService locationService = new LocationService();



		public LocationServiceTest(ITestOutputHelper output)
		{
			this.output = output;
		}



		[Theory]
		[MemberData(nameof(LocationServiceTestData.CreateLocationTestData), MemberType = typeof(LocationServiceTestData))]
		public async Task TestCreateLocation_ReturnNoException(Location location, List<Location> subLocations)
		{
			try
			{
				await locationService.SaveLocation(location, subLocations);
				output.WriteLine("Test case passed");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}







		[Theory]
		[MemberData(nameof(LocationServiceTestData.UpdateLocationTestData), MemberType = typeof(LocationServiceTestData))]
		public async Task TestUpdateLocation_ReturnNoException(int id, Location location)
		{

			await locationService.UpdateLocation(id, location);
			SELMsContext a = new SELMsContext();

			var loc = await a.Locations.FirstOrDefaultAsync(l => l.location_id == id);

			if (loc != null)
			{
				Assert.Equal(location.location_code, loc.location_code);
				Assert.Equal(location.is_active, loc.is_active);
				output.WriteLine("Update successfull");
			}
			else
				output.WriteLine("Location not found to update");
		}





		[Theory]
		[MemberData(nameof(LocationServiceTestData.CreateEquipLocationHistoryTestData), MemberType = typeof(LocationServiceTestData))]
		public async Task TestSaveEquipLocationHistory_ReturnNoException(Equipment_Location_History item)
		{
			try
			{
				await locationService.SaveEquipLocationHistory(item);
				output.WriteLine("Test case passed");
			}
			catch (Exception ex)
			{
				Assert.Fail("Test case failed\n" + ex.Message);
			}
		}
	}



	internal static class LocationServiceTestData
	{
		public static IEnumerable<object[]> CreateLocationTestData()
		{
			Location l1 = new() { location_code = "DE-213", is_active = true };
			Location l2 = new() { location_code = "DE-214", is_active = true };
			Location l3 = new() { location_code = "DE-215", is_active = false };


			List<Location> list1 = new()
			{
				new Location() { location_code = "BE-210", is_active = false },
				new Location() { location_code = "BE-211", is_active = true }
			};

			List<Location> list2 = new() { new Location() { location_code = "BE-111", is_active = true } };
			List<Location> list3 = new() { new Location() { location_code = "BE-113", is_active = false } };


			yield return new object[] { l1, list1 };
			yield return new object[] { l2, list2 };
			yield return new object[] { l3, list3 };
		}







		public static IEnumerable<object[]> UpdateLocationTestData()
		{
			Location l1 = new() { location_code = "", is_active = true };
			Location l2 = new() { location_code = "DE-330", is_active = false };


			List<Location> list1 = new() { new Location() { location_code = "BE-104", is_active = false }, };
			List<Location> list2 = new() { new Location() { location_code = "BE-107", is_active = true } };


			yield return new object[] { 0, l1, list1 };
			yield return new object[] { 5, l2, list2 };
		}



		public static IEnumerable<object[]> CreateEquipLocationHistoryTestData()
		{
			yield return new object[] { new Equipment_Location_History() { system_equipment_code = "E0052", location_id = 4 } };
			yield return new object[] { new Equipment_Location_History() { system_equipment_code = "E0010", location_id = 5 } };
			yield return new object[] { new Equipment_Location_History() { system_equipment_code = "E0088", location_id = 4 } };
		}
	}
}

