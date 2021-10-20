using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CyclingResults.Domain;

namespace CyclingResults.SampleData
{
    public static class EventCollection
    {
        public static List<Event> Events { get; private set; }

        static int EventIdGenerator = 0;

        public static List<Race> Races { get; private set; }

        static int RaceIdGenerator = 0;

        public static List<Result> Results { get; private set; }

        static int ResultIdGenerator = 0;

        public static List<RaceStatistics> RaceStatistics { get; private set; }

        static int RaceStatisticsIdGenerator = 0;

        static EventCollection()
        {
            Events = new List<Event>();
            Races = new List<Race>();
            Results = new List<Result>();
            RaceStatistics = new List<RaceStatistics>();

            CreateEventOneSample();
        
            // TODO add another one here!
        }

        static int GenerateEventId()
        {
            return ++EventIdGenerator;
        }
        
        static int GenerateRaceId()
        {
            return ++RaceIdGenerator;
        }

        static int GenerateResultId()
        {
            return ++ResultIdGenerator;
        }

        static int GenerateStatisticsId()
        {
            return ++RaceStatisticsIdGenerator;
        }

        static void CreateEventOneSample()
        {
            var eventInstance = new Event
            {
                Id = GenerateEventId(),
                Date = new DateTime(2021, 9, 26),
                Name = "Ego Cross",
                Races = new List<Race>()
            };

            var raceInstance = new Race
            {
                Id = GenerateRaceId(),
                Name = "B (Men)",
                StartTime = new DateTime(eventInstance.Date.Year, eventInstance.Date.Month, eventInstance.Date.Day, 12, 0, 0),
                Results = new List<Result>(),
                Laps = 5
            };

            var resultInstance = new Result
            {
                Id = GenerateResultId(),
                FirstName = "Mathew",
                LastName = "Grabau",
                PlateNumber = 166.ToString(),
                TeamName = "FOG",
                ResultTime = (long)Math.Round(new TimeSpan(0, 46, 35).TotalMilliseconds, 0),
                LicenseVerified = true,
                Started = true,
                Place = 25,
                LapsCompleted = 5
            };

            raceInstance.Results.Add(resultInstance);

            resultInstance = new Result
            {
                Id = GenerateResultId(),
                FirstName = "Gary",
                LastName = "Sewell",
                PlateNumber = 138.ToString(),
                ResultTime = (long)Math.Round(new TimeSpan(0, 46, 44).TotalMilliseconds, 0),
                LicenseVerified = true,
                Started = true,
                Place = 26,
                LapsCompleted = 5
            };

            raceInstance.Results.Add(resultInstance);

            Races.Add(raceInstance);

            // Link it into the instance of it.
            eventInstance.Races.Add(raceInstance);

            RaceStatistics stats = CalculateRaceStatistics(raceInstance);

            RaceStatistics.Add(stats);

            raceInstance = new Race
            {
                Id = GenerateRaceId(),
                Name = "B (Women)",
                StartTime = new DateTime(eventInstance.Date.Year, eventInstance.Date.Month, eventInstance.Date.Day, 12, 0, 0),
                Results = new List<Result>()
            };

            Races.Add(raceInstance);

            eventInstance.Races.Add(raceInstance);

            
            Events.Add(eventInstance);
        }

        static RaceStatistics CalculateRaceStatistics(Race race)
        {
            decimal lapAverage = 0;
            decimal cummulativeAverage = 0;

            foreach(var result in race.Results)
            {
                lapAverage += result.CalculateAverageLapTime();
                cummulativeAverage += result.ResultTime;
            }

            if (race.Results.Count > 0)
            {
                lapAverage /= race.Results.Count;
                cummulativeAverage /= race.Results.Count;
            }

            return new RaceStatistics
            {
                Id = GenerateStatisticsId(),
                RaceId = race.Id,
                AverageLapTime = (long)lapAverage,
                AverageTime = (long)cummulativeAverage
            };
        }
    }
}
