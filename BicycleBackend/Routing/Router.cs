using System;
using System.Collections.Generic;
using System.IO;
using OsmSharp.Osm.PBF.Streams;

namespace BicycleBackend.Routing
{
    public class Router
    {
        public string PathToMapData => $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\bicyclebicycle\\hamiltonmap";

        public Router()
        {
            using (var fileStream = new FileInfo(PathToMapData).OpenRead())
            {
                var source = new PBFOsmStreamSource(fileStream);
                foreach (var element in source)
                {
                    Console.WriteLine(element.ToString());
                }
            }
        }

        public IEnumerable<Segment> Route(double startLat, double startLon, double endLat, double endLon)
        {
            return new Segment[0];
        }
    }
}