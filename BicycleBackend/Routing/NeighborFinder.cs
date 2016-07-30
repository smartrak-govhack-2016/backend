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

                var filter = new OsmStreamFilterTagsFilter(TagsFilter);
                filter.RegisterSource(source);
                foreach (OsmGeo element in filter.ToArray())
                {
                    var way = element as Way;
                    if (way == null)
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
                            _pointToNeighbors[lastPoint.Value].Add(segment);
                            _pointToNeighbors[point].Add(segment);
                        }
                        lastPoint = point;
                    }
                }
            }
        }

        private void TagsFilter(TagsCollectionBase collection)
        {
            foreach (var thing in ThingsWeThinkAreSwell)
            {
                if (collection.ContainsKey(thing))
                {
                    // do something maybe?
                }
            }
        }

        public IEnumerable<Segment> FindNeighbors(Segment segment)
        {
            return _pointToNeighbors[segment.Start].Union(_pointToNeighbors[segment.End]);
        }

        public Segment FindNearestNeighbor(double lat, double lon)
        {
            throw new NotImplementedException();
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