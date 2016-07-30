using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BicycleBackend.Routing
{
    public class Segment
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public Safety SafetyRating { get; set; }
        public string StreetName { get; set; }
    }
}