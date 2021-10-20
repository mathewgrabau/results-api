namespace CyclingResults.Domain
{
    public class RaceStatistics
    {
        public int Id { get; set; }

        public int RaceId { get; set; }

        public long AverageTime { get; set; }

        public long AverageLapTime { get; set; }
    }
}
