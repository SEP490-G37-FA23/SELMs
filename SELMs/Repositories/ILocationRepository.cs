using SELMs.Models;
using SELMs.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELMs.Repositories
{
    public interface ILocationRepository
    {
        dynamic GetLocationList();
        dynamic GetLocationList(Argument arg);
        dynamic GetLocation(int id);
        dynamic SaveLocation(Location location);
        void UpdateLocation(Location location);
        void DeleteLocation(int id);
    }
}
