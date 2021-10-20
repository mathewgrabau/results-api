using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

using CyclingResults.Domain;

namespace CyclingResults.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;

        public EventController(ILogger<EventController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Event> Get(int? id)
        {
            if (id.HasValue && id.Value > 0)
            {
                return SampleData.EventCollection.Events.Where(e => e.Id == id);
            }

            return SampleData.EventCollection.Events.OrderBy(e => e.Id);
        }

        [HttpGet("{id}/races")]
        public IEnumerable<Race> GetRaces(int id)
        {
            return SampleData.EventCollection.Events.FirstOrDefault(e => e.Id == id)?.Races.OrderBy(r => r.Id);
        }
    }
}
