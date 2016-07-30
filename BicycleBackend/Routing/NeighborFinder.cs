using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        //        public string PathToMapData => $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\bicyclebicycle\\new-zealand-latest.osm.pbf";
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

        public NeighborFinder()
        {
            using (var fileStream = new FileInfo(PathToMapData).OpenRead())
            {
                var source = new XmlOsmStreamSource(fileStream);
                var filter = new OsmStreamFilterTagsFilter(TagsFilter);
                filter.RegisterSource(source);
                foreach (OsmGeo element in filter.ToArray())
                {
                    // add way
                    // add nodes
                    // add nodes to neighbor dictionary
//                    element.
//                    if (!element.Tags.Any(x => ThingsWeThinkAreSwell.Contains(x)))
                        Debug.WriteLine(element.ToString());
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
            throw new NotImplementedException();
        }

        public Segment FindNearestNeighbor(double lat, double lon)
        {
            throw new NotImplementedException();
        }
    }
}