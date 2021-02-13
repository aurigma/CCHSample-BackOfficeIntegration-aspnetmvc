using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomersCanvasSample.Models
{
    public class DesignModel
    {
        public DesignModel(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public DesignModel(string id, string name, string uifConfig)
        {
            Id = id;
            Name = name;
            Config = uifConfig;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Config { get; private set; }
    }
}
