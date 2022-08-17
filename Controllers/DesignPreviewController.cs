using System.IO;
using System.Threading.Tasks;
using Aurigma.AssetProcessor;
using Microsoft.AspNetCore.Mvc;

namespace CustomersCanvasSample.Controllers
{
    public class DesignPreviewController : Controller
    {
        private readonly IDesignProcessorApiClient _designProcessorApiClient;

        public DesignPreviewController(IDesignProcessorApiClient designProcessorApiClient)
        {
            _designProcessorApiClient = designProcessorApiClient;
        }

        public async Task<Stream> Index(string id, int? surfaceIndex)
        {
            const string previewNamespace = "demoapp";
            const string previewName = "thumbnail";
            const int previewWidth = 600;
            const int previewHeight = 600;

            var previewBody = await _designProcessorApiClient.PreparePreviewAsync(
                id,
                previewNamespace,
                previewName,
                previewWidth,
                previewHeight,
                surfaceIndex: surfaceIndex,
                force: true, // slower, but show all surfaces
                //stub: true, // faster, but don't work with multi surface states
                format: DesignPreviewFormat.Jpeg);

            return previewBody.Stream;
        }
    }
}
