using CustomersCanvasSample.Models;
using CustomersCanvasSample.Services;
using Microsoft.AspNetCore.Mvc;
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
                ConnectedProducts = await _ecommerceDataService.GetConnectedProducts()
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
    }
}
