using System;

namespace BicycleBackend.Routing
{
    public static class Distance
    {
        /// <summary>
        /// returns distance in meters
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <param name="otherLat"></param>
        /// <param name="otherLon"></param>
        /// <returns></returns>
        public static double Haversine(double lat, double lon, double otherLat, double otherLon)
        {
            double R = 6371;
            var tempLat = (otherLat - lat).ToRadians();
            var tempLon = (otherLon - lon).ToRadians();
            var h1 = Math.Sin(tempLat / 2) * Math.Sin(tempLat / 2) +
                          Math.Cos(lat.ToRadians()) * Math.Cos(otherLat.ToRadians()) *
                          Math.Sin(tempLon / 2) * Math.Sin(tempLon / 2);
            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
            return R * h2 * 1000;
        }

        private static double ToRadians(this double val)
        {
            return (Math.PI / 180) * val;
        }
    }
}