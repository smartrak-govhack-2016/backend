﻿using System.Collections.Generic;

namespace BicycleBackend.Routing
{
    public class Segment
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public Safety SafetyRating { get; set; }
		/// <summary>
		/// Bigger is better
		/// </summary>
		public double Weight { get; set; }
        public string StreetName { get; set; }

        public int IncedentCount { get; set; } = 1;

        public double GetSafetyWeight => Weight/IncedentCount;

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

		public Segment ReversedClone()
		{
			return new Segment
			{
				Start = End,
				End = Start,
				SafetyRating = SafetyRating,
				StreetName = StreetName,
				Weight = Weight
			};
		}
    }
}