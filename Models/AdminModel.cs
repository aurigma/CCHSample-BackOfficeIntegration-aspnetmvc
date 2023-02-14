using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersCanvasSample.Models
{
    public class AdminModel
    {
        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<Product> ConnectedProducts { get; set; }

        public IEnumerable<Product> ConnectedPimProducts { get; set; }
    }
}
