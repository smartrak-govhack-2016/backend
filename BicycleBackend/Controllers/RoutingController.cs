using System.Web.Http;

namespace BicycleBackend.Controllers
{
    public class RoutingController : ApiController
    {
        [HttpGet]
        [Route("v1/route/{startlat},{startlon},{endlat},{endlon}")]
        public IHttpActionResult GetRoute(double startLat, double startLon, double endLat, double endLon)
        {
            return Ok($"({startLat},{startLon}),({endLat},{endLon})");
        }
    }
}
