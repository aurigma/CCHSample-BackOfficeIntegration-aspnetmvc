﻿using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Aurigma.DesignAtomsApi;
using Microsoft.AspNetCore.Mvc;

namespace CustomersCanvasSampleMVC.Controllers
{
    public class DesignAtomsApiController : Controller
    {
        private readonly IDesignAtomsServiceApiClient _apiClient;

        public DesignAtomsApiController(IDesignAtomsServiceApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [HttpGet]
        public async Task<ActionResult> ExtractBackground([FromRoute] string id, [FromQuery] int? surfaceIndex, CancellationToken cancellationToken)
        {
            FileResponse fileResponse;
            try
            {
                fileResponse = await _apiClient
                    .ExtractBackgroundAsync(id, surfaceIndex: surfaceIndex, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (ApiClientException apiClientException)
            {
                if (apiClientException is ApiClientException<MissingDesignElementDto> missingElement)
                {
                    var elementType = missingElement.Result.MissingElementType;
                    var description = missingElement.Result.Description;

                    return NotFound($"{elementType} is missing: {description}");
                }

                throw;
            }

            var headers = fileResponse.Headers;
            
            var contentType = headers["Content-Type"].FirstOrDefault();

            var dispositionType = headers["Content-Disposition"].FirstOrDefault();
            var contentDisposition = new ContentDisposition(dispositionType);
            var fileName = contentDisposition.FileName;

            return File(fileResponse.Stream, contentType, fileName);
        }
    }
}
