using CustomersCanvasSample.Models;
using CustomersCanvasSample.Services;
using CustomersCanvasSampleMVC.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersCanvasSample.Controllers
{
    public class AdminController : Controller
    {
        private readonly EcommerceDataService _ecommerceDataService;

        public AdminController(
            EcommerceDataService ecommerceDataService)
        {
            _ecommerceDataService = ecommerceDataService;
        }

        public async Task<IActionResult> Index()
        {
            return View(new AdminModel()
            {
                Products = _ecommerceDataService.GetProducts(),
                ConnectedProducts = await _ecommerceDataService.GetConnectedProducts(),
                ConnectedPimProducts = await _ecommerceDataService.GetConnectedPimProducts()
            });
        }

        public async Task<IActionResult> Edit(string id)
        {
            return View(await _ecommerceDataService.GetProductData(id));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductDetails(string id, string name, float price)
        {
            _ecommerceDataService.UpdateProductDetails(id, name, price);

            return View("Edit", await _ecommerceDataService.GetProductData(id));
        }

        [HttpPost]
        public async Task<IActionResult> ConnectProduct(string productId, int productSpecificationId)
        {
            await _ecommerceDataService.ConnectProduct(productId, productSpecificationId);

            return View("Edit", await _ecommerceDataService.GetProductData(productId));
        }

        [HttpPost]
        public async Task<IActionResult> DisconnectProduct(string productId)
        {
            await _ecommerceDataService.DisconnectProduct(productId);

            return View("Edit", await _ecommerceDataService.GetProductData(productId));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateIntegrationDetails(string productId, string productEditorType)
        {
            _ecommerceDataService.UpdateEditorType(productId, Enum.Parse<EditorType>(productEditorType));

            return View("Edit", await _ecommerceDataService.GetProductData(productId));
        }
    }
}
