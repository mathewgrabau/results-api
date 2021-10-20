using System;
using System.Collections.Generic;

namespace CyclingResults.Domain
{
    /// <summary>
    /// An event holder 
    /// </summary>
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // The date the event took place.
        public DateTime Date { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public List<Race> Races { get; set; }
    }
}
