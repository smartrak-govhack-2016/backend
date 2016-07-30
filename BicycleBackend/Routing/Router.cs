using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Kts.AStar;
using OsmSharp.Collections.Tags;
using OsmSharp.Osm;
using OsmSharp.Osm.PBF.Streams;
using OsmSharp.Osm.Streams.Filters;
using OsmSharp.Osm.Xml.Streams;

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
            throw new NotImplementedException();
        }

        private double GetHScore(Segment segment, Segment otherSegment)
        {
            throw new NotImplementedException();
        }
    }
}