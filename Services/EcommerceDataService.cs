using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aurigma.StorefrontApi;
using Aurigma.StorefrontApi.Products;
using CustomersCanvasSample.Db;
using CustomersCanvasSample.Models;
using CustomersCanvasSampleMVC.Enums;
using Microsoft.Extensions.Options;

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
        private readonly Aurigma.StorefrontApi.IProductReferencesApiClient _productReferencesApiClient;

        private readonly CustomersCanvasOptions _options;
        private readonly IProductsApiClient _productsApiClient;

        public EcommerceDataService(
            EcommerceContext dbContext,
            IProductSpecificationsApiClient productSpecificationsApiClient,
            Aurigma.StorefrontApi.IProductReferencesApiClient productReferencesApiClient,
            IProductsApiClient productsApiClient,
            IOptions<CustomersCanvasOptions> options)
        {
            _dbContext = dbContext;
            _productSpecificationsApiClient = productSpecificationsApiClient;
            _productReferencesApiClient = productReferencesApiClient;
            _options = options.Value;
            _productsApiClient = productsApiClient;
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

        public async Task<IEnumerable<Product>> GetConnectedPimProducts()
        {
            var products = GetProducts();

            var pimStorefrontProductIds = new List<string>();

            var pimProducts = await _productsApiClient.GetAllProductsAsync(tenantId: 10140);

            foreach (var pimProduct in pimProducts.Items)
            {
                var links = await _productsApiClient.GetProductLinksAsync(pimProduct.Id, tenantId: 10140);

                pimStorefrontProductIds.AddRange(links.Items.Select(x => x.StorefrontProductId));
            }

            return products.Where(p => pimStorefrontProductIds.Any(id => id == p.Id)).ToList();
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

        public void UpdateEditorType(string productId, EditorType editorType)
        {
            var product = GetProductById(productId);
            product.EditorType = editorType;
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

            result.ProductIntegrationType = await GetProductIntegrationType(productId);

            return result;
        }

        private async Task<ProductIntegrationType> GetProductIntegrationType(string productId)
        {
            if (await IsProductPimIntegrated(productId))
                return ProductIntegrationType.PIM;

            if (await IsProductIntegrated(productId))
                return ProductIntegrationType.ProductSpecification;

            return ProductIntegrationType.NoIntegration;
        }

        private async Task<bool> IsProductPimIntegrated(string productId)
        {
            var pimStorefrontProductIds = new List<string>();

            var pimProducts = await _productsApiClient.GetAllProductsAsync(tenantId: 10140);

            foreach (var pimProduct in pimProducts.Items)
            {
                var links = await _productsApiClient.GetProductLinksAsync(pimProduct.Id, tenantId: 10140);

                pimStorefrontProductIds.AddRange(links.Items.Select(x => x.StorefrontProductId));
            }

            return pimStorefrontProductIds.Any(id => id == productId);
        }

        private async Task<bool> IsProductIntegrated(string productId)
        {
            var references = await _productReferencesApiClient
                .GetAllAsync(_options.StorefrontId, productReference: productId);

            return references.Items.Any();
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
        public static int OrderIdToNumber(string id)
        {
            return Math.Max(1001, int.Parse(id) - 12344677);
        }

        public static string CustomerIdToName(string id)
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
