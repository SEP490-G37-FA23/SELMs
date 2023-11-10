using SELMs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELMs.Services
{
    public interface ILocationService
    {
        Task SaveLocation(Location location, List<Location> subLocations);
        Task UpdateLocation(int id, Location location, List<Location> subLocations);
        Task SaveEquipLocationHistory(Equipment_Location_History item);


    }
}
