using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.Api.DTOs
{
    public class LocationRequest
    {
        public LocationDTO Location { get; set; }
        public List<LocationDTO> SubLocations { get; set; }
    }
}