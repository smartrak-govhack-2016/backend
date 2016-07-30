using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BicycleBackend.Routing
{
    public class Segment
    {
        public double StartLat { get; set; }
        public double StartLon { get; set; }
        public Safety SafetyRating { get; set; }
        public string StreetName { get; set; }
    }
}