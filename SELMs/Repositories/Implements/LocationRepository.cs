using Dapper;
using SELMs.Api.DTOs;
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
            dynamic locations = db.Locations.ToListAsync();
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

        public dynamic SaveLocations(List<Location> locations)
        {
            db.Locations.AddRange(locations);
            db.SaveChanges();
            return locations;
        }

        public dynamic GetLocationList(Argument arg)
        {
            dynamic categories = null;
            categories = db.Database.Connection.QueryAsync<dynamic>("Proc_GetLocationList", new
            {
                username = arg.username,
                parent_id = arg.id,
                location_name = arg.text
            }
                , commandType: CommandType.StoredProcedure);
            return categories;
        }

        public dynamic GetAllSubLocationList(Argument arg)
        {
            dynamic categories = null;
            categories = db.Database.Connection.QueryAsync<dynamic>("Proc_GetAllSubLocationList", new
            {
                username = arg.username,
                parent_id = arg.id,
                location_name = arg.text
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

        public List<LocationDTO> GetListSubLocation(int id)
        {
            List<LocationDTO> ListSubLocation = new List<LocationDTO>();
            ListSubLocation = db.Database.Connection.Query<LocationDTO>("Proc_GetLocationList", new
            {

                username = "",
                parent_id = id,
                location_name = ""
            }
                , commandType: CommandType.StoredProcedure).ToList();
            return ListSubLocation;
        }

        public List<ProjectDTO> GetListProjectInLocation(int id)
        {
            List<ProjectDTO> ListProjectInLocation = new List<ProjectDTO>();
            ListProjectInLocation = db.Database.Connection.Query<ProjectDTO>("Proc_GetListProjectInLocation", new
            {
                location_id= id,
                        }
                , commandType: CommandType.StoredProcedure).ToList();
            return ListProjectInLocation;
        }

        public List<EquipmentDTO> GetListEquipInLocation(int id)
        {
            List<EquipmentDTO> ListEquipInLocation = new List<EquipmentDTO>();
            ListEquipInLocation = db.Database.Connection.Query<EquipmentDTO>("Proc_GetListEquipInLocation", new
            {
                location_id = id,
            }
                , commandType: CommandType.StoredProcedure).ToList();
            return ListEquipInLocation;
        }

        public dynamic GetEquipment_Location_History(string system_equipment_code, int location_id)
        {
            dynamic his = db.Equipment_Location_History.Where(l => l.location_id == location_id && l.system_equipment_code == system_equipment_code).FirstOrDefault();
            return his;
        }

        public dynamic AddNewEquipLocationHistory(Equipment_Location_History item)
        {
            Equipment_Location_History his = db.Equipment_Location_History.Where(l => l.system_equipment_code == item.system_equipment_code).FirstOrDefault();
            his.to_date = DateTime.Now;
            item.from_date = DateTime.Now;
            db.Equipment_Location_History.Add(item);
            db.SaveChanges();
            return item;
        }

        public void UpdateEquipLocationHistory(Equipment_Location_History item)
        {

            Equipment_Location_History his = db.Equipment_Location_History.Where(l => l.location_id == item.location_id && l.system_equipment_code == item.system_equipment_code).FirstOrDefault();
            his.from_date = DateTime.Now;
            db.Entry(his).CurrentValues.SetValues(item);
            db.SaveChangesAsync();
        }
    }
}