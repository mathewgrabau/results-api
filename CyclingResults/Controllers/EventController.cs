using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

using CyclingResults.Domain;
using System;
using CyclingResults.Domain.Repository;
using System.Threading.Tasks;

namespace CyclingResults.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly IRepository<Event> _eventRepository;

        public EventController(ILogger<EventController> logger, IRepository<Event> eventRepository)
        {
            _logger = logger;
            _eventRepository = eventRepository;
        }

        [HttpGet]
        public IEnumerable<Event> Get(int? id)
        {
            if (id.HasValue && id.Value > 0)
            {
                //return SampleData.EventCollection.Events.Where(e => e.Id == id);
                var result = new List<Event>();
                result.Add(_eventRepository.Get(id.Value));
                return result;
            }


            return _eventRepository.GetAll();

            //if (id.HasValue && id.Value > 0)
            //{
            //    return SampleData.EventCollection.Events.Where(e => e.Id == id);
            //}

            //return SampleData.EventCollection.Events.OrderBy(e => e.Id);
        }

        [HttpGet("{id}/races")]
        public IActionResult GetRaces(int id)
        {
            Event eventObject = _eventRepository.Get(id);
            if (eventObject == null)
            {
                return NotFound();
            }

            return Ok(eventObject.Races);
            //return SampleData.EventCollection.Events.FirstOrDefault(e => e.Id == id)?.Races.OrderBy(r => r.Id);
        }


        [HttpPost()]
        public async Task<IActionResult> Create(Event eventObject)
        {
            if (eventObject == null)
            {
                return BadRequest();
            }

            if (eventObject.Id == 0)
            {
                var result = await _eventRepository.Add(eventObject);

                if (result.Id > 0)
                {
                    return Ok();
                }

                // TODO need a different return result (and should also log that something went wrong here).
                return Ok();
            }

            return BadRequest();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Event eventObject)
        {
            if (eventObject == null)
            {
                return BadRequest();
            }

            bool updated = await _eventRepository.Update(eventObject);

            if (updated)
            {
                return Ok();
            }

            return Ok();
        }
    }
}
