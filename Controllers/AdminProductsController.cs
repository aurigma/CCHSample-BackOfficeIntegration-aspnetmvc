using Aurigma.BackOffice;
using CustomersCanvasSample.Models;
using CustomersCanvasSample.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomersCanvasSample.Controllers
{
    public class AdminProductsController : Controller
    {
        private EcommerceDataService ecommerceDataService;

        public AdminProductsController(
            EcommerceDataService ecommerceDataService)
        {
            this.ecommerceDataService = ecommerceDataService;
        }

        public IActionResult Index()
        {
            return View(ecommerceDataService.GetTestProducts());
        }
        public async Task<IActionResult> Edit(string id)
        {
            return View(await ecommerceDataService.GetAdminProduct(id));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductDetails(string id, string name, float price)
        {
            ecommerceDataService.UpdateProductDetails(id, name, price);
            return View("Edit", await ecommerceDataService.GetAdminProduct(id));
        }

        [HttpPost]
        public async Task<IActionResult> ConnectTemplate(string productId, int templateId) 
        {
            await ecommerceDataService.ConnectTemplate(productId, templateId);
            return View("Edit", await ecommerceDataService.GetAdminProduct(productId));
        }

        [HttpPost]
        public async Task<IActionResult> DisconnectTemplate(string productId, int templateId)
        {
            await ecommerceDataService.DisconnectTemplate(productId, templateId);
            return View("Edit", await ecommerceDataService.GetAdminProduct(productId));
        }
    }
}
