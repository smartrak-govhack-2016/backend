namespace BicycleBackend.Routing
{
    public class Segment
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public Safety SafetyRating { get; set; }
        public string StreetName { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Segment;
            if (other == null)
                return false;
            return other.Start.Equals(Start) && other.End.Equals(End) && other.SafetyRating == SafetyRating &&
                   other.StreetName == StreetName;
        }

        public override int GetHashCode()
        {
            return Start.GetHashCode() ^ End.GetHashCode();
        }
    }
}