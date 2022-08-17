using System;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Aurigma.DesignAtomsApi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

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

                if (apiClientException.StatusCode == (int)HttpStatusCode.UnprocessableEntity)
                {
                    var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(apiClientException.Response);
                    return UnprocessableEntity(problemDetails.Detail);
                }

                if (apiClientException is ApiClientException<Aurigma.DesignAtomsApi.ProblemDetails> generalProblem)
                {
                    return BadRequest($"Api return status code '{generalProblem.Result.Status}' and message: {generalProblem.Result.Detail}");
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
