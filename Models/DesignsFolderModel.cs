using Aurigma.AssetStorage;

namespace CustomersCanvasSample.Models
{
    public class DesignsFolderModel
    {
        public DesignsFolderModel(string path, FolderContentOfDesignDto content)
        {
            Path = path;
            Content = content;
        }

        public string Path { get; }
        public FolderContentOfDesignDto Content { get; }
    }
}
