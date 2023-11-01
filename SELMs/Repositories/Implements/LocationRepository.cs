using SELMs.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SELMs.Repositories.Implements
{
    public class LocationRepository : ILocationRepository
    {
        private SELMsContext db = new SELMsContext();
        public dynamic GetLocationList()
        {
            dynamic locations = db.Locations.ToList();
            return locations;
        }
    }
}