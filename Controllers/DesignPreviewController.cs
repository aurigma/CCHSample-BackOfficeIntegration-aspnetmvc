using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aurigma.AssetProcessor;
using System.IO;

namespace DesignBrowserMVC.Controllers
{
    public class DesignPreviewController : Controller
    {
        private readonly IDesignProcessorApiClient _designProcessorApiClient;
        public DesignPreviewController(IDesignProcessorApiClient designProcessorApiClient)
        {
            _designProcessorApiClient = designProcessorApiClient;
        }

        public async Task<Stream> Index(string id)
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
                true,
                DesignPreviewFormat.Jpeg, false);

            return previewBody.Stream;
        }
    }
}
