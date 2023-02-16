using Aurigma.StorefrontApi;
using CustomersCanvasSampleMVC.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersCanvasSample.Models
{
    public class ProductData
    {
        public Product Product { get; set; }
        public ProductReferenceDto Reference { get; set; }
        public IEnumerable<ProductSpecificationDto> Specifications { get; set; }
        public ProductIntegrationType ProductIntegrationType { get; set; }
    }
}