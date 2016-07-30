﻿using System.Web.Http;
using BicycleBackend.Db;
using BicycleBackend.Routing;

namespace BicycleBackend.Controllers
{
    public class RoutingController : ApiController
    {
        private readonly CrashContext _context;
        private Router _router;

        public RoutingController()
        {
            _context = new CrashContext();
            _router = new Router(new NeighborFinder());
        }

        // segment
        // name 
        // danger level 1,2,3. Enum / text
        [HttpGet]
        [Route("v1/route/{startlat},{startlon},{endlat},{endlon}")]
        public IHttpActionResult GetRoute(double startLat, double startLon, double endLat, double endLon)
        {
            return Ok(_router.Route(startLat, startLon, endLat, endLon));
        }

        [HttpGet]
        [Route("v1/route/crashes")]
        public IHttpActionResult GetCrashes()
        {
            return Ok(_context.GetCrashes());
        }
    }
}
