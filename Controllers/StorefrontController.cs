using CustomersCanvasSample.Services;
using CustomersCanvasSampleMVC.Enums;
using CustomersCanvasSampleMVC.Services;
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
        private readonly DataSourceService _dataSourceService;
        private CustomersCanvasOptions _options;

        public StorefrontController(
            EcommerceDataService ecommerceDataService,
            DataSourceService dataSourceService,
            IOptions<CustomersCanvasOptions> options)
        {
            _ecommerceDataService = ecommerceDataService;
            _dataSourceService = dataSourceService;
            _options = options.Value;
        }

        public async Task<IActionResult> Index()
        {
            var connectedProducts = await _ecommerceDataService.GetConnectedProducts();
            var connectedPimProducts = await _ecommerceDataService.GetConnectedPimProducts();

            return View(connectedProducts.Union(connectedPimProducts));
        }

        public IActionResult Edit(string id)
        {
            var product = _ecommerceDataService.GetProductById(id);

            ViewBag.TenantId = _options.TenantId;
            ViewBag.BackOfficeUrl = _options.IdentityProviderUrl;
            ViewBag.EcommerceSystemId = _options.StorefrontId;

            // It is supposed that user is authorised.
            // Otherwise the session should be generated.
            ViewBag.UserId = 1234;

            return product.EditorType switch
            {
                EditorType.UIFramework => View(product),
                EditorType.SimpleEditor => View("SimpleEditor", product),
                _ => throw new NotImplementedException(),
            };
        }

        public IActionResult Personalize(string id, bool LoadDataToTemplate)
        {
            ViewBag.TenantId = _options.TenantId;
            ViewBag.BackOfficeUrl = _options.IdentityProviderUrl;
            ViewBag.LoadDataToTemplate = LoadDataToTemplate;

            // It is supposed that user is authorised.
            // Otherwise the session should be generated.
            ViewBag.UserId = 1234;
            ViewBag.Data = _dataSourceService.GetDataForTemplate();

            return View(_ecommerceDataService.GetProductById(id));
        }
    }
}
