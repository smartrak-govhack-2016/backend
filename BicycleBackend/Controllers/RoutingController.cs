using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using BicycleBackend.Db;
using BicycleBackend.Routing;

namespace BicycleBackend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RoutingController : ApiController
    {
        private static CrashContext _context;
        private static Router _router;
        private static CircleRouter _circleRouter;

        public RoutingController()
        {
            _context = Cache.CrashContext;
            _router = Cache.Router;
            _circleRouter = Cache.CircleRouter;
        }

        [HttpGet]
        [Route("v1/route/{startlat}/{startlon}/{endlat}/{endlon}")]
        public IHttpActionResult GetRoute(double startLat, double startLon, double endLat, double endLon)
        {
            try
            {
                var route = _router.Route(startLat, startLon, endLat, endLon);

                return Ok(route);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

		[HttpGet]
		[Route("v1/circle/{startlat}/{startlon}")]
		public IHttpActionResult GetCircleRoute(double startLat, double startLon)
		{
			try
			{
				var route = _circleRouter.FindCircleRoute(startLat, startLon);

				return Ok(route);
			}
			catch (Exception ex)
			{
				return BadRequest();
			}
		}
		[HttpGet]
        [Route("v1/route/crashes")]
        public IHttpActionResult GetCrashes()
        {
            return Ok(_context.GetCrashes());
        }
    }
}
