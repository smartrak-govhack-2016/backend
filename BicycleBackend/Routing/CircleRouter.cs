using System;
using System.Collections.Generic;
using System.Linq;
using Kts.AStar;

namespace BicycleBackend.Routing
{
    public class CircleRouter
    {
        private readonly INeighborFinder _neighborFinder;

        public CircleRouter(INeighborFinder neighborFinder)
        {
            _neighborFinder = neighborFinder;
        }

		List<Segment> currentRoute;
		double currentRouteDist = 0;


		public IEnumerable<Segment> FindCircleRoute(double startLat, double startLon)
		{
			currentRoute = new List<Segment>();
			currentRouteDist = 0;

			var start = _neighborFinder.FindNearestNeighbor(startLat, startLon);
			var currentEnd = start;

			while (true)
			{
				//Pick something a bit away
				Segment end = null;
				var rand = new Random();
				while (end == null)
				{
					end = _neighborFinder.FindNearestNeighbor(currentEnd.End.Lat + (rand.NextDouble() - 0.5) * 0.01, currentEnd.End.Lon + (rand.NextDouble() - 0.5) * 0.01);
				}

				var route = Route(currentEnd.Start.Lat, currentEnd.Start.Lon, end.End.Lat, end.End.Lon);
				var routeDist = route.Sum(segment => Distance.Haversine(segment.Start.Lat, segment.Start.Lon, segment.End.Lat, segment.End.Lon) / 1000.0);

				var endToStart = Route(route.Last().End.Lat, route.Last().End.Lon, startLat, startLon);
				var endToStartDist = endToStart.Sum(segment => Distance.Haversine(segment.Start.Lat, segment.Start.Lon, segment.End.Lat, segment.End.Lon) / 1000.0);

				var totalDist = currentRouteDist + routeDist + endToStartDist;
				if (totalDist >= 4 && totalDist <= 6)
				{
					//Found an end
					currentRoute.AddRange(route);
					currentRoute.AddRange(endToStart);
					return currentRoute;
				}
				else if (totalDist < 4)
				{
					//Add a bit more on, not too far away yet
					currentRoute.AddRange(route);
					currentEnd = route.Last();
					currentRouteDist += routeDist;
				} else
				{
					//Fuck it
				}
			}
		}


		private List<Segment> Route(double startLat, double startLon, double endLat, double endLon)
        {
            double distance;
            bool success;
            var start = _neighborFinder.FindNearestNeighbor(startLat, startLon);
            var goal = _neighborFinder.FindNearestNeighbor(endLat, endLon);
            return AStarUtilities.FindMinimalPath(start, goal, _neighborFinder.FindNeighbors, GetScore, segment => GetHScore(segment, goal), out distance, out success);
        }

        private double GetScore(Segment segment, Segment otherSegment)
        {
            return LengthOfSegment(segment) + LengthOfSegment(otherSegment);
        }

        private double GetHScore(Segment segment, Segment otherSegment)
        {
            return DistanceBetweenSegments(segment, otherSegment);
        }

        private double DistanceBetweenSegments(Segment segment, Segment otherSegment)
        {
            return Distance.Haversine(segment.Start.Lat, segment.Start.Lon, otherSegment.Start.Lat,
                otherSegment.Start.Lon);
        }

        private double LengthOfSegment(Segment segment)
        {
	        double weight = currentRoute.Contains(segment) ? 0.4 : 1;

			//TODO: Weight against going down a road we have gone down
			return Distance.Haversine(segment.Start.Lat, segment.Start.Lon, segment.End.Lat,
                segment.End.Lon) / segment.Weight / weight;
        }
    }
}