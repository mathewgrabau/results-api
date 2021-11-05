using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

using CyclingResults.Domain;
using CyclingResults.Domain.Repository;
using System.Threading.Tasks;

namespace CyclingResults.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultController : ControllerBase
    {
        private readonly ILogger<ResultController> _logger;
        private readonly IRepository<Result> _resultRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="resultRepository"></param>
        public ResultController(ILogger<ResultController> logger, IRepository<Result> resultRepository)
        {
            _logger = logger;
            _resultRepository = resultRepository;
        }

        /// <summary>
        /// Retuns a collection of results that the user can access.
        /// </summary>
        /// <param name="id">The id (0 to return all available result records that the user has permission to access.</param>
        /// <returns>A collection the </returns>
        [HttpGet("{id}")]
        public IEnumerable<Result> Get(int id)
        {
            if (id > 0)
            {
                var temp = _resultRepository.Get(id);
                if (temp == null)
                {
                    return Array.Empty<Result>();
                }

                return new Result[] { temp };
            }

            return _resultRepository.GetAll();
        }

        // TODO need to add the ability to change it.
    }
}
