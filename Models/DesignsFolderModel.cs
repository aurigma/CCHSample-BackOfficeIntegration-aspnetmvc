using Aurigma.AssetStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersCanvasSample.Models
{
    public class DesignsFolderModel
    {
        public DesignsFolderModel(string path, FolderContentOfDesignDto content)
        {
            Path = path;
            Content = content;
        }

        public string Path { get; private set; }
        public FolderContentOfDesignDto Content { get; private set; }
    }
}
