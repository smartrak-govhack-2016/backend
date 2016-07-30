using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OsmSharp.Collections.Tags;
using OsmSharp.Osm;
using OsmSharp.Osm.Streams.Filters;
using OsmSharp.Osm.Xml.Streams;

namespace BicycleBackend.Routing
{
    public interface INeighborFinder
    {
        IEnumerable<Segment> FindNeighbors(Segment segment);
        Segment FindNearestNeighbor(double lat, double lon);
    }
    public class NeighborFinder : INeighborFinder
    {
        public string PathToMapData => $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\bicyclebicycle\\hamiltonmap";

        private List<string> ThingsWeThinkAreSwell => new List<string>()
        {
            "cycleway",
            "cycleway:left",
            "bicycle",
            "highway",
            "maxspeed",
            "junction",
            "roundabout"
        };

        private Dictionary<Point, IList<Segment>> _pointToNeighbors;
        private NnFinder _nnFinder;

        public NeighborFinder()
        {
            _pointToNeighbors = new Dictionary<Point, IList<Segment>>();

            using (var fileStream = new FileInfo(PathToMapData).OpenRead())
            {
                var source = new XmlOsmStreamSource(fileStream);
                var nodes = new Dictionary<long, Node>();
                foreach (var thing in source)
                {
                    if(thing is Node)
                    {
                        var node = (Node)thing;
                        nodes[node.Id.Value] = node;
                        _pointToNeighbors[new Point(node)] = new List<Segment>();
                    }
                }
                List<Segment> allSegments = new List<Segment>();
                foreach (OsmGeo element in source.Where(x => x.Type == OsmGeoType.Way))
                {
                    if (element.Type == OsmGeoType.Way)
                    {
                        Console.WriteLine("wtf");
                    }
                    var way = element as Way;
                    if (way == null || !WeCareAboutThisTypeOfWay(way))
                        return;
                    Point? lastPoint = null;
                    foreach (var nodeId in way.Nodes)
                    {
                        var point = new Point(nodes[nodeId]);

                        if (lastPoint != null)
                        {
                            Segment segment = new Segment
                            {
                                Start = lastPoint.Value,
                                End = point
                            };
                            allSegments.Add(segment);
                            _pointToNeighbors[lastPoint.Value].Add(segment);
                            _pointToNeighbors[point].Add(segment);
                        }
                        lastPoint = point;
                    }
                }
                _nnFinder = new NnFinder(allSegments);
            }
        }

        private bool WeCareAboutThisTypeOfWay(Way way)
        {
            return way.Tags.ContainsOneOfKeys(ThingsWeThinkAreSwell);
        }

        public IEnumerable<Segment> FindNeighbors(Segment segment)
        {
            return _pointToNeighbors[segment.Start].Union(_pointToNeighbors[segment.End]);
        }

        public Segment FindNearestNeighbor(double lat, double lon)
        {
            return _nnFinder.NearestSegment(lat, lon);
        }
    }
    public struct Point
    {
        public double Lat { get; }
        public double Lon { get; }
        public Point(Node node)
        {
            Lat = node.Latitude.Value;
            Lon = node.Longitude.Value;
        }
    }
}