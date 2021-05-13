using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersCanvasSample.Models
{
    public class ProjectItemModel
    {
        public IDictionary<string, object> Fields { get; set; }
        public object Hidden { get; set; }
        public IEnumerable<string> StateId { get; set; }
    }
}
