using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace CyclingResults.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultController : ControllerBase
    {
        private readonly ILogger<ResultController> _logger;

        public ResultController(ILogger<ResultController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Retuns an 
        /// </summary>
        /// <param name="eventId">Returns the result of an event</param>
        /// <returns></returns>
        [HttpGet("/{id}")]
        public IEnumerable<Result> Get(int id)
        {
            return SampleData.EventCollection.Results.Where(r => r.Id == id);
        }

        // TODO need to add the ability to change it.
    }

}
