using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CustomersCanvasSample.Models;
using Aurigma.AssetStorage;
using Microsoft.Extensions.Options;
using Aurigma.AssetProcessor;

namespace CustomersCanvasSample.Controllers
{
    public class DesignsController : Controller
    {
        private readonly ILogger<DesignsController> _logger;
        private readonly IDesignsApiClient _designsApiClient;
        private readonly IDesignProcessorApiClient _designProcessorApiClient;

        private const string _path = "/";
        private readonly CustomersCanvasOptions _ccoptions;

        public DesignsController(
            ILogger<DesignsController> logger, 
            IDesignsApiClient designsApiClient, 
            IDesignProcessorApiClient designProcessorApiClient,
            IOptions<CustomersCanvasOptions> customersCanvasOptions)
        {
            _logger = logger;
            _designsApiClient = designsApiClient;
            _designProcessorApiClient = designProcessorApiClient;
            _ccoptions = customersCanvasOptions.Value;
        }

        public async Task<IActionResult> Index()
        {
            var path = _path;
            var folder = await _designsApiClient.GetFolderAsync(path);
            return View(new Models.DesignsFolderModel(path, folder));
        }
        public IActionResult Edit(string id, string name)
        {
            ViewBag.TenantId = _ccoptions.TenantId;
            ViewBag.DesignEditorVersion = _ccoptions.DesignEditorVersion;
            return View(new DesignModel(id, name));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
