using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

using CyclingResults.Domain;
using CyclingResults.Domain.Repository;
using System.Threading.Tasks;
using CyclingResults.Models.Repository;

namespace CyclingResults.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RaceController : ControllerBase
    {
        private readonly ILogger<RaceController> _logger;
        private readonly IEventRepository _eventRepository;
        private readonly IRepository<Race> _raceRepository;
        private readonly IRepository<ResultUpload> _uploadRepository;

        public RaceController(ILogger<RaceController> logger, IEventRepository eventRepository, IRepository<Race> raceRepository, IRepository<ResultUpload> uploadRepository)
        {
            _logger = logger;
            _eventRepository = eventRepository;
            _raceRepository = raceRepository;
            _uploadRepository = uploadRepository;
        }

        [HttpGet("{id}")]
        public IEnumerable<Race> Get(int? id)
        {
            if (id == 0)
            {
                return _raceRepository.GetAll();
            }

            return new List<Race>() { _raceRepository.Get(id.Value) };
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

        /// <summary>
        /// Post a new result to the API. Must contain the event that you are currently posting for when creating the event that makes a lot of sense.
        /// </summary>
        /// <returns></returns>
        [HttpPost("{eventId}")]
        public async Task<IActionResult> Post(int eventId, [FromBody] Race raceObject)
        {
            if (raceObject == null)
            {
                return BadRequest();
            }

            raceObject.EventId = eventId;

            var result = await _raceRepository.Add(raceObject);

            if (result == null)
            {
                if (result.Id == 0)
                {
                    return BadRequest();
                }
            }

            return Ok();
        }

        /// <summary>
        /// Posts a result for it.
        /// </summary>
        /// <param name="raceId"></param>
        /// <returns></returns>
        [HttpPost("{id}/result")]
        public async Task<IActionResult> PostResult(int id, [FromForm] string url, [FromForm] string description)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return BadRequest();
            }

            // Adding a result for posting a result for it
            var raceInstance = _raceRepository.Get(id);

            if (raceInstance == null)
            {
                return BadRequest();
            }

            // TODO how can we validate that it is being handled correctly on the return.
            var result = await _uploadRepository.Add(new ResultUpload
            {
                Race = raceInstance,
                Description = description,
                Url = url
            });

            if (result != null)
            {
                return BadRequest();
            }

            if (result.Id > 0)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
