using System;

namespace BicycleBackend.Db
{
    public class Crash
    {
        public int _Id { get; set; }
        public DateTime CrashDate { get; set; }
        public int CrashTime { get; set; }
        public string ObjectsStruck { get; set; }
        public string RoadWet { get; set; }
        public string WthRa { get; set; }
        public int SpdLim { get; set; }
        public int CrashFatalCnt { get; set; }
        public int CrashSevCnt { get; set; }
        public int CrashMinCnt { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}