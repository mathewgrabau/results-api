using CyclingResults.Domain;
using CyclingResults.Domain.Repository;
using CyclingResults.Models.Repository;
using CyclingResults.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyclingResults.Controllers
{
    public class UploadResultModel
    {
        public string Url { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class ResultUploadController : ControllerBase
    {
        private readonly ILogger<ResultUploadController> _logger;
        private readonly AzureOCRService _ocrService;
        private readonly IResultUploadRepository _uploadRepository;
        private readonly IRepository<Race> _raceRepository;
            
        /// <summary>
        /// Controller constructor.
        /// </summary>
        /// <param name="logger">Injected instance of logger interface.</param>
        public ResultUploadController(ILogger<ResultUploadController> logger, IResultUploadRepository uploadRepository, IRepository<Race> raceRepository, AzureOCRService ocrService)
        {
            _logger = logger;
            _raceRepository = raceRepository;
            _uploadRepository = uploadRepository;
            _ocrService = ocrService;
            
        }

        [HttpGet("{uploadId}")]
        public async Task<IActionResult> Get(int uploadId)
        {
            if (uploadId == 0)
            {
                // This produces the listing.
                return Ok(_uploadRepository.GetAll());
            }

            // This gets the result object.
            var upload = _uploadRepository.Get(uploadId);

            if (upload != null)
            {
                // We need to try to get the results here.
                var serviceResult = await _ocrService.GetResult(upload.ResultId.Value);

                if (serviceResult == null)
                {
                    return NotFound("Error on Azure service - result may be unavailable/expored. Upload again");
                }

                // TODO we need a temp table to be able to store the results of the operation.
                // Even if it's just a blob, that would do.

                // TODO need a different result type from there.

                List<ExtractedLine> extactedLines = new List<ExtractedLine>();

                // TODO how should we accept processing the information?
                // Easy, if succeeded, then we can injest the lines that seem to have actual columns.
                // Bounding box information can help us determine the column information.
                // For now, return the lines into it.
                if (serviceResult.Status == Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models.OperationStatusCodes.Succeeded)
                {
                    foreach (var readResult in serviceResult.AnalyzeResult.ReadResults)
                    {
                        foreach (var line in readResult.Lines)
                        {
                            // Need the bounding rectangles around it.

                            ExtractedLine extractedLine = new ExtractedLine
                            {
                                Text = line.Text,
                                Words = line.Words.Select(x => x.Text).ToArray(),
                                WordConfidence = line.Words.Select(x => x.Confidence).ToArray(),
                                BoundingBoxes = line.Words.Select(x=>x.BoundingBox).ToArray()
                            };

                            extactedLines.Add(extractedLine);
                        }
                    }
                }
                else
                {
                    _logger.LogWarning("Azure processing was not available for " + uploadId + " : " + serviceResult.Status.ToString());
                }

                return Ok(extactedLines);
            }

            return BadRequest();
        }


        public class ExtractedLine
        {
            public string Text { get; set; }
            public string[] Words { get; set; }
            public double[] WordConfidence { get; set; }
            public IList<double?>[] BoundingBoxes { get; set; }
            public decimal Accuracy { get; set; }
        }

        /// Posts a request to get upload.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{raceId}")]

        public async Task<IActionResult> Post(int raceId, [FromBody] UploadResultModel model)
        {
            Race raceEntity = _raceRepository.Get(raceId);

            if (raceEntity == null)
            {
                return BadRequest();
            }

            if (model.Url != null)
            {
                var readHeaders = await _ocrService.AnalyzeImage(model.Url);

                Uri resultPath = new Uri(readHeaders.OperationLocation);

                var pathParts = resultPath.AbsolutePath.Split("/");

                string finalPath = pathParts[pathParts.Length - 1];

                Guid resultId;

                ResultUpload resultUpload = null;

                if (Guid.TryParse(finalPath, out resultId))
                {
                    resultUpload = new ResultUpload()
                    {
                        RaceId = raceId,
                        Url = model.Url,
                        ResultId = resultId,
                        CreatedOn = DateTimeOffset.UtcNow,
                    };

                    resultUpload = await _uploadRepository.Add(resultUpload);
                }

                if (resultUpload != null)
                {
                    // TODO need toa actually capture things.
                    return Ok(resultUpload);
                }

                // TODO figure out if this is the right ones.
                return Problem("OCR attempt failed");
               
            }

            return Ok();
        }

        /// <summary>
        /// Delete an upload record from the system (assuming it has been uploaded previously).
        /// </summary>
        /// <param name="uploadId"></param>
        /// <returns></returns>
        [HttpDelete("{uploadId}")]
        public async Task<IActionResult> Delete(int uploadId)
        {
            if (_uploadRepository.Delete(uploadId))
            {
                return Ok();
            }

            return Delete();
        }

    }
}
