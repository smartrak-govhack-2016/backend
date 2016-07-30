using System.Web.Http;
using BicycleBackend.Db;

namespace BicycleBackend.Controllers
{
    public class RoutingController : ApiController
    {
        private readonly CrashContext _context;

        public RoutingController()
        {
            _context = new CrashContext();
        }

        // segment
        // name 
        // danger level 1,2,3. Enum / text
        [HttpGet]
        [Route("v1/route/{startlat},{startlon},{endlat},{endlon}")]
        public IHttpActionResult GetRoute(double startLat, double startLon, double endLat, double endLon)
        {
            return Ok($"({startLat},{startLon}),({endLat},{endLon})");
        }

        [HttpGet]
        [Route("v1/route/crashes")]
        public IHttpActionResult GetCrashes()
        {
            return Ok(_context.GetCrashes());
        }
    }
}
