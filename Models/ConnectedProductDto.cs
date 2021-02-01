using Aurigma.BackOffice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersCanvasSample.Models
{
    public class ConnectedProductDto
    {
        public Product Product { get; set; }
        public TemplateDto Template { get; set; }
    }
}
