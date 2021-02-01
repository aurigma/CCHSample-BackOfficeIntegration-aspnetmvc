using Aurigma.BackOffice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersCanvasSample.Models
{
    public class ProjectDetailsModel
    {
        public IEnumerable<ProjectProductParametersDto> LineItems { get; set; }
        public string OrderId { get; set; }
        public string UserId { get; set; }
        public string ProductId { get; set; }
    }
}
