using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesignBrowserMVC.Models
{
    public class DesignModel
    {
        public DesignModel(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
    }
}
