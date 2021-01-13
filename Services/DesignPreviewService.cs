using Aurigma.AssetProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesignBrowserMVC.Services
{
    public class DesignPreviewService
    {
        private readonly IDesignProcessorApiClient _designProcessorApiClient;

        public DesignPreviewService(IDesignProcessorApiClient designProcessorApiClient)
        {
            _designProcessorApiClient = designProcessorApiClient;
        }

        public async Task<FileResponse> GetPreviewUrlFor(string id)
        {
            const string previewNamespace = "demoapp";
            const string previewName = "thumbnail";
            const int previewWidth = 600;
            const int previewHeight = 600;

            return await _designProcessorApiClient.PreparePreviewAsync(
                id,
                previewNamespace,
                previewName,
                previewWidth,
                previewHeight,
                true,
                DesignPreviewFormat.Jpeg);
        }

    }
}
