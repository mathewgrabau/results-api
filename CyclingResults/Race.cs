using System;
using System.Collections.Generic;

namespace CyclingResults
{

    public class RaceStatistics
    {
        public int Id { get; set; }

        public int RaceId { get; set; }

        public long AverageTime { get; set; }

        public long AverageLapTime { get; set; }
    }

    public class Race
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // When the event started
        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string Category { get; set; }

        /// <summary>
        /// </summary>
        public string RaceType { get; set; }

        public string Classification { get; set; }

        public int Laps { get; set; }

        public List<Result> Results { get; set; }
    }

    /// <summary>
    /// Contains/wraps a service.
    /// </summary>
    public class Result
    {
        // This one might actually need to be a Guid due to the volume of results there.
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        /// <summary>
        /// A string containing the plate number for it.
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// The string indicating which team the user is associated with for the race.
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// The time (in milliseconds) for the user's time.
        /// </summary>
        public long ResultTime { get; set; }

        /// <summary>
        /// Whether the license for the rider was verified at the start line.
        /// </summary>
        public bool LicenseVerified { get; set; }

        /// <summary>
        /// Supports tracking a DNS instance.
        /// </summary>
        public bool Started { get; set; }

        public int LapsCompleted { get; set; }

        public int? Place { get; set; }

        /// <summary>
        /// Calculates the average lap time for this racers result.
        /// </summary>
        /// <returns></returns>
        public long CalculateAverageLapTime()
        {
            if (LapsCompleted > 0)
            {
                return ResultTime / LapsCompleted;
            }

            return long.MaxValue;  
        }
    }
}
