using CustomersCanvasSample.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersCanvasSample.Controllers
{
    public class StorefrontController : Controller
    {
        private EcommerceDataService _ecommerceDataService;
        private CustomersCanvasOptions _options;

        public StorefrontController(
            EcommerceDataService ecommerceDataService,
            IOptions<CustomersCanvasOptions> options)
        {
            _ecommerceDataService = ecommerceDataService;
            _options = options.Value;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _ecommerceDataService.GetConnectedProducts());
        }

        public IActionResult Edit(string id)
        {
            return View(_ecommerceDataService.GetProductById(id));
        }

        public IActionResult Personalize(string id)
        {
            ViewBag.TenantId = _options.TenantId;
            ViewBag.BackOfficeUrl = _options.IdentityProviderUrl;

            return View(_ecommerceDataService.GetProductById(id));
        }
    }
}
