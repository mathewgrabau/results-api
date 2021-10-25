using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

using CyclingResults.Domain;
using System;
using CyclingResults.Domain.Repository;
using System.Threading.Tasks;
using CyclingResults.Models.Repository;

namespace CyclingResults.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly IEventRepository _eventRepository;

        public EventController(ILogger<EventController> logger, IEventRepository eventRepository)
        {
            _logger = logger;
            _eventRepository = eventRepository;
        }

        [HttpGet("{id}")]
        public IEnumerable<Event> Get(int id)
        {
            if (id > 0)
            {
                //return SampleData.EventCollection.Events.Where(e => e.Id == id);
                var result = new List<Event>();
                var eventObject = _eventRepository.Get(id, true);
                if (eventObject != null)
                {
                    foreach(var race in eventObject.Races)
                    {
                        if (race.Event != null)
                        {
                            race.Event = null;
                        }
                    }

                    result.Add(eventObject);
                }
                return result;
            }

            // Need to be able to cache it around there.
            var collection = _eventRepository.GetAll(true);

            if (collection != null)
            {
                foreach (var eventItem in collection)
                {
                    foreach(var race in eventItem.Races)
                    {
                        if (race.Event != null)
                        {
                            race.Event = null;
                        }
                    }
                }
            }

            return collection;

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
        public async Task<IActionResult> Update(int id, [FromBody] Event eventObject)
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
