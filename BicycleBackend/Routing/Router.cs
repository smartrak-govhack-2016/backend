using System.Collections.Generic;
using Kts.AStar;

namespace BicycleBackend.Routing
{
    public class Router
    {
        private readonly INeighborFinder _neighborFinder;

        public Router(INeighborFinder neighborFinder)
        {
            _neighborFinder = neighborFinder;
        }

        public IEnumerable<Segment> Route(double startLat, double startLon, double endLat, double endLon)
        {
            var start = _neighborFinder.FindNearestNeighbor(startLat, startLon);
            var goal = _neighborFinder.FindNearestNeighbor(endLat, endLon);
            return AStarUtilities.FindMinimalPath(start, goal, _neighborFinder.FindNeighbors, GetScore, segment => GetHScore(segment, goal)).Result;
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
            return Distance.Haversine(segment.Start.Lat, segment.Start.Lon, segment.End.Lat,
                segment.End.Lon);
        }
    }
}