using Aurigma.AssetProcessor;
using Aurigma.AssetStorage;
using CustomersCanvasSample.Models;
using CustomersCanvasSample.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersCanvasSample.Controllers
{
    public class DesignsController : Controller
    {
        private readonly IDesignsApiClient _designsApiClient;
        private readonly IDesignProcessorApiClient _designProcessorApiClient;
        private readonly CustomersCanvasOptions _options;

        private const string _path = "/";

        public DesignsController(
            IDesignsApiClient designsApiClient,
            IDesignProcessorApiClient designProcessorApiClient,
            IOptions<CustomersCanvasOptions> customersCanvasOptions)
        {
            _designsApiClient = designsApiClient;
            _designProcessorApiClient = designProcessorApiClient;
            _options = customersCanvasOptions.Value;
        }

        public async Task<IActionResult> Index()
        {
            var path = _path;
            var folder = await _designsApiClient.GetFolderAsync(path);

            return View(new DesignsFolderModel(path, folder));
        }

        public IActionResult Edit(string id, string name)
        {
            ViewBag.TenantId = _options.TenantId;
            ViewBag.DesignEditorVersion = _options.DesignEditorVersion;

            return View(new DesignModel(id, name));
        }

        public IActionResult EditWithUIF(string id, string name, string configName)
        {
            ViewBag.TenantId = _options.TenantId;
            ViewBag.DesignEditorVersion = _options.DesignEditorVersion;

            using var uiFrameworkConfigService = UIFrameworkService.FromAppData(
                Path.Combine(
                    AppDomain.CurrentDomain.GetData("DataDirectory").ToString(),
                    "ui-framework",
                    configName
                ));

            return View(new DesignModel(
                id,
                name,
                uiFrameworkConfigService.GetConfigAsJsonString(true)));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
