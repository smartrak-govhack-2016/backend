using System;
using System.Collections.Generic;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using NetTopologySuite.Index;
using NetTopologySuite.Index.Strtree;

namespace BicycleBackend.Routing
{
    public class NnFinder
    {
        private readonly IEnumerable<Segment> _segments;
        private const double ExtendBy = 0.002;
        private STRtree<Segment> _tree;

        public NnFinder(IEnumerable<Segment> segments)
        {
            _segments = segments;
            BuildTree();
        }

        private void BuildTree()
        {
            _tree = new STRtree<Segment>();
            foreach (var segment in _segments)
            {
                _tree.Insert(ToLineString(segment).EnvelopeInternal, segment);
            }
            _tree.Build();
        }

        public Segment NearestSegment(double lat, double lon)
        {
            double minLat = lat - ExtendBy, maxLon = lon + ExtendBy, minLon = lon - ExtendBy, maxLat = lat + ExtendBy;

            Visitor<Segment> visitor = new Visitor<Segment>(new NetTopologySuite.Geometries.Point(lon, lat), ToLineString);

            _tree.Query(new Envelope(minLon, maxLon, minLat, maxLat), visitor);

            return visitor.Closest;
        }

        private LineString ToLineString(Segment segment)
        {
            return new LineString(new[] { new Coordinate(segment.Start.Lon, segment.Start.Lat), new Coordinate(segment.End.Lon, segment.End.Lat) });
        }

        private class Visitor<T> : IItemVisitor<T> where T : class
        {
            private readonly NetTopologySuite.Geometries.Point _point;
            private Func<T, IGeometry> _converter;
            public double MinimumDistance = 10000000.0 * 1000;
            public T Closest = null;

            public Visitor(NetTopologySuite.Geometries.Point point, Func<T, IGeometry> converter)
            {
                _point = point;
                _converter = converter;
            }

            public void VisitItem(T item)
            {
                var res = NetTopologySuite.Operation.Distance.DistanceOp.NearestPoints(_point, _converter(item));
                double dist = Distance.Haversine(res[0].Y, res[0].X, res[1].Y, res[1].X);
                if (dist < MinimumDistance)
                {
                    MinimumDistance = dist;
                    Closest = item;
                }
            }
        }
    }
}