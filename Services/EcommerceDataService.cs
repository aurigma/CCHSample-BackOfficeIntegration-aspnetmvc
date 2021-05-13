using Aurigma.StorefrontApi;
using CustomersCanvasSample.Db;
using CustomersCanvasSample.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersCanvasSample.Services
{
    /// <summary>
    /// This is a stub implementation of a service of your ecommerce system which works with the 
    /// customer and order information. You need to replace it with your business logic here.
    /// </summary>
    public class EcommerceDataService
    {
        private readonly EcommerceContext _dbContext;

        private readonly IProductSpecificationsApiClient _productSpecificationsApiClient;
        private readonly IProductReferencesApiClient _productReferencesApiClient;

        private readonly CustomersCanvasOptions _options;

        public EcommerceDataService(
            EcommerceContext dbContext,
            IProductSpecificationsApiClient productSpecificationsApiClient,
            IProductReferencesApiClient productReferencesApiClient,
            IOptions<CustomersCanvasOptions> options)
        {
            _dbContext = dbContext;
            _productSpecificationsApiClient = productSpecificationsApiClient;
            _productReferencesApiClient = productReferencesApiClient;
            _options = options.Value;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _dbContext.Products.ToList();
        }

        public async Task<IEnumerable<Product>> GetConnectedProducts()
        {
            var products = GetProducts();

            var productReferences = await _productReferencesApiClient.GetAllAsync(_options.StorefrontId);
            if (productReferences.Total == 0)
            {
                return new List<Product>();
            }

            var ids = productReferences.Items.Select(r => r.ProductReference).ToList();

            return products.Where(p => ids.Any(id => id == p.Id)).ToList();
        }

        public Product GetProductById(string productId)
        {
            return _dbContext.Products.First(p => p.Id == productId);
        }

        public void UpdateProductDetails(string productId, string name, float price)
        {
            var product = GetProductById(productId);
            product.Name = name;
            product.Price = price;
            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
        }

        public async Task<ProductData> GetProductData(string productId)
        {
            var references = await _productReferencesApiClient.GetAllAsync(_options.StorefrontId, productReference: productId);
            var specifications = await _productSpecificationsApiClient.GetAllAsync();

            var result = new ProductData();
            result.Product = GetProductById(productId);
            result.Reference = references.Items?.FirstOrDefault();
            result.Specifications = specifications.Total > 0
                ? specifications.Items
                : new List<ProductSpecificationDto>();
            return result;
        }

        public async Task ConnectProduct(string productId, int productSpecificationId)
        {
            var body = new CreateProductReferenceDto()
            {
                ProductReference = productId,
                ProductSpecificationId = productSpecificationId,
            };

            await _productReferencesApiClient.CreateAsync(_options.StorefrontId, body: body);
        }

        public async Task DisconnectProduct(string productId)
        {
            await _productReferencesApiClient.DeleteAsync(productId, _options.StorefrontId);
        }

        // start your numbers from 12345678
        static public int OrderIdToNumber(string id)
        {
            return Math.Max(1001, int.Parse(id) - 12344677);
        }

        static public string CustomerIdToName(string id)
        {
            switch (id)
            {
                case "1234":
                    return "Alice";
                case "2345":
                    return "Bob";
                default:
                    return "Some user";
            }
        }
    }
}
