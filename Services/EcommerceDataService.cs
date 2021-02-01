using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aurigma.BackOffice;
using CustomersCanvasSample.Db;
using CustomersCanvasSample.Models;
using Microsoft.Extensions.Options;

namespace CustomersCanvasSample.Services
{
    /// <summary>
    /// This is a stub implementation of a service of your ecommerce system which works with the 
    /// customer and order information. You need to replace it with your business logic here.
    /// </summary>
    public class EcommerceDataService
    {
        private ITemplatesApiClient templatesApiClient;
        private IEcommerceProductReferencesApiClient productReferenceApiClient;
        private CustomersCanvasOptions ccoptions;
        private EcommerceContext dbcontext;

        public EcommerceDataService(EcommerceContext dbcontext, ITemplatesApiClient templatesApiClient,
            IEcommerceProductReferencesApiClient productReferenceApiClient,
            IOptions<CustomersCanvasOptions> ccoptions)
        {
            this.dbcontext = dbcontext;
            this.templatesApiClient = templatesApiClient;
            this.productReferenceApiClient = productReferenceApiClient;
            this.ccoptions = ccoptions.Value;
        }

        public IEnumerable<Product> GetTestProducts()
        {
            return dbcontext.Products.ToList();
        }

        public IEnumerable<Product> GetConnectedProducts()
        {
            return dbcontext.Products.Where(p => p.TemplateId != null).ToList();
        }

        public Product GetProductById(string id)
        {
            return dbcontext.Products.First(p => p.Id == id);
        }

        public void UpdateProductDetails(string id, string name, float price)
        {
            var product = GetProductById(id);
            product.Name = name;
            product.Price = price;
            dbcontext.Products.Update(product);
            dbcontext.SaveChanges();
        }

        public async Task<AdminProductModel> GetAdminProduct(string id)
        {
            var model = new AdminProductModel();
            model.Product = GetProductById(id);
            model.Templates = await templatesApiClient.GetAllAsync();
            return model;
        }

        public async Task ConnectTemplate(string productId, int templateId)
        {
            await productReferenceApiClient.CreateAsync(productId, templateId, ccoptions.StorefrontId);
            UpdateTemplateId(productId, templateId);
        }

        public async Task DisconnectTemplate(string productId, int templateId)
        {
            await productReferenceApiClient.DeleteAsync(productId, templateId, ccoptions.StorefrontId);
            UpdateTemplateId(productId, null);
        }

        private void UpdateTemplateId(string productId, int? templateId)
        {
            var product = GetProductById(productId);
            product.TemplateId = templateId;
            dbcontext.Products.Update(product);
            dbcontext.SaveChanges();
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
