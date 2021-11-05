using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Rest;

namespace CyclingResults.Services
{

    /// <summary>
    /// Provides a service that will quiery Azure for upload requests.
    /// </summary>
    public class AzureOCRService
    {
        private readonly ILogger<AzureOCRService> _logger;
        ComputerVisionClient _azureClient;
        private readonly ApiKeyServiceClientCredentials _credentials;
        private readonly string _endpoint;

        public AzureOCRService(IConfiguration configuration, ILogger<AzureOCRService> logger)
        {
            _logger = logger;
            string apiKey = configuration["AzureOCRKey"];
            _credentials = new ApiKeyServiceClientCredentials(apiKey);
            _endpoint = configuration["AzureOCREndpoint"];
        }

        public async Task<ReadHeaders> AnalyzeImage(string imageUrl)
        {
            using (_azureClient = new ComputerVisionClient(_credentials) { Endpoint = _endpoint })
            {
                var readHeaders = await _azureClient.ReadAsync(imageUrl);

                _logger.LogInformation("The result is " + readHeaders.OperationLocation);

                return readHeaders;
            }

            return null;
        }

        //public object GetStatus(Guid? resultId)
        //{
        //    using (_azureClient = new ComputerVisionClient(_credentials) { Endpoint = _endpoint })
        //    {
        //        _azureClient.GetReadResultAsync()
        //    }
        //}

        // TODO obviously this will have to change (so we can use different platforms).
        public async Task<ReadOperationResult> GetResult(Guid operationId)
        {
            using (_azureClient = new ComputerVisionClient(_credentials) { Endpoint = _endpoint })
            {
                try
                {
                    ReadOperationResult azureResult = await _azureClient.GetReadResultAsync(operationId);
                    return azureResult;
                }
                catch (ComputerVisionOcrErrorException e)
                {
                    _logger.LogError("{0}", e);

                    return null;
                }
            }
        }
    }
}
