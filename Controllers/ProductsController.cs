using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomersCanvasSample.Services;
using Microsoft.Extensions.Options;

namespace CustomersCanvasSample.Controllers
{
    public class ProductsController : Controller
    {
        private CustomersCanvasOptions ccoptions;
        private EcommerceDataService ecommerceDataService;
        public ProductsController(EcommerceDataService ecommerceDataService, IOptions<CustomersCanvasOptions> ccoptions)
        {
            this.ecommerceDataService = ecommerceDataService;
            this.ccoptions = ccoptions.Value;
        }

        public IActionResult Index()
        {
            return View(ecommerceDataService.GetConnectedProducts());
        }
        public IActionResult Edit(string id)
        {
            return View(ecommerceDataService.GetProductById(id));
        }

        public IActionResult Personalize(string id)
        {
            ViewBag.TenantId = ccoptions.TenantId;
            ViewBag.BackOfficeUrl = ccoptions.IdentityProviderUrl;

            return View(ecommerceDataService.GetProductById(id));
        }

    }
}
