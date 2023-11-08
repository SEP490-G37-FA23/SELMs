using Dapper;
using SELMs.Models;
using SELMs.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Data;
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
        public void DeleteLocation(int id)
        {
            dynamic location = db.Locations.Where(l => l.location_id == id).FirstOrDefault();
            if (location != null) db.Locations.Remove(location);
            db.SaveChangesAsync();
        }

        public dynamic GetLocation(int id)
        {
            dynamic location = db.Locations.Where(l => l.location_id == id).FirstOrDefault();
            return location;
        }

        public dynamic SaveLocation(Location location)
        {
            db.Locations.Add(location);
            db.SaveChanges();
            return location;
        }

        public dynamic GetLocationList(Argument arg)
        {
            dynamic categories = null;
            categories = db.Database.Connection.QueryAsync<dynamic>("Proc_GetLocationList", new
            {
                username = arg.username,
            }
                , commandType: CommandType.StoredProcedure);
            return categories;
        }

        public void UpdateLocation(Location location)
        {
            Location orgLocation = db.Locations.Where(l => l.location_id == location.location_id).FirstOrDefault();
            db.Entry(orgLocation).CurrentValues.SetValues(location);
            db.SaveChangesAsync();
        }
    }
}