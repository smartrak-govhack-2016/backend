using System;
using System.Collections.Generic;
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

        public RoutingController()
        {
            _context = Cache.CrashContext;
            _router = Cache.Router;
        }

        [HttpGet]
        [Route("v1/route/{startlat}/{startlon}/{endlat}/{endlon}")]
        public IHttpActionResult GetRoute(double startLat, double startLon, double endLat, double endLon)
        {
            try
            {
                var route = _router.Route(startLat, startLon, endLat, endLon);

                return Ok(SortRoute(route));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        private List<Segment> SortRoute(List<Segment> route)
        {
            if (route != null && route.Count >= 2)
            {
                var result = new List<Segment>();
                if (route[0].Start.Equals(route[1].Start) || route[0].Start.Equals(route[1].End))
                {
                    //Reverse the 1st
                    result.Add(route[0].ReversedClone());
                }
                else
                {
                    result.Add(route[0]);
                }

                //Now sort the rest of them
                for (var i = 1; i < route.Count; i++)
                {
                    var last = result.Last();
                    var now = route[i];
                    if (last.End.Equals(now.Start))
                    {
                        result.Add(now);
                    }
                    else
                    {
                        result.Add(now.ReversedClone());
                    }
                }
                route = result;
            }
            return route;
        }

        [HttpGet]
		[Route("v1/circle/{startlat}/{startlon}")]
		public IHttpActionResult GetCircleRoute(double startLat, double startLon)
		{
			try
			{
				var route = new CircleRouter(Cache.NeighborFinder).FindCircleRoute(startLat, startLon).ToList();

			    FixDupes(route);

				return Ok(SortRoute(route));
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

        private void FixDupes(List<Segment> route)
        {
            for (var i = 0; i < route.Count - 1; i++)
            {
                if (route[i] == route[i + 1])
                {
                    route.RemoveAt(i);
                }
            }
        }
    }
}
