using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace CyclingResults.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RaceController : ControllerBase
    {
        private readonly ILogger<RaceController> _logger;

        public RaceController(ILogger<RaceController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public IEnumerable<Race> Get(int? id)
        {
            if (id == 0)
            {
                return SampleData.EventCollection.Races.ToList();
            }
            return SampleData.EventCollection.Races.Where(r => r.Id == id);
        }

        // TODO need to develop the corresponding information.
        [HttpGet("{id}/results")]
        public IEnumerable<Result> GetResults(int id)
        {
            return SampleData.EventCollection.Races.Where(r => r.Id == id).FirstOrDefault()?.Results.OrderBy(result => result.Place);
        }

        // TODO need to figure out the single instance.
        [HttpGet("{id}/stats")]
        public IEnumerable<RaceStatistics> GetStatistics(int id)
        {
            return SampleData.EventCollection.RaceStatistics.Where(race => race.Id == id);
        }
    }
}
