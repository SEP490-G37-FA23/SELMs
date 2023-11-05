using SELMs.Models;
using SELMs.Repositories;
using SELMs.Repositories.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SELMs.Services.Implements
{
    public class LocationService : ILocationService
    {
        private ILocationRepository repository = new LocationRepository();

        public async Task SaveLocation(Location location)
        {
            Location eq = location;
            repository.SaveLocation(eq);
        }

        public async Task UpdateLocation(int id, Location location)
        {
            if (await repository.GetLocation(id) != null)
            {
                location.location_id = id;
                repository.UpdateLocation(location);
            }
        }
    }
}