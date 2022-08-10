using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace CustomersCanvasSample.Configuration
{
    public class CustomersCanvasApiClientConfiguration:
        Aurigma.AssetStorage.IApiClientConfiguration,
        Aurigma.AssetProcessor.IApiClientConfiguration,
        Aurigma.StorefrontApi.IApiClientConfiguration,
        Aurigma.DesignAtomsApi.IApiClientConfiguration
    {
        private CustomersCanvasOptions _options;
        private TokenService _tokenService;

        public CustomersCanvasApiClientConfiguration(
            TokenService tokenService,
            IOptions<CustomersCanvasOptions> customersCanvasOptions)
        {
            _tokenService = tokenService;
            _options = customersCanvasOptions.Value;
        }

        public string GetApiKey()
        {
            return "";
        }

        public string GetApiUrl()
        {
            return _options.ApiUrl;
        }

        public Task<string> GetAuthorizationTokenAsync()
        {
            return _tokenService.GetTokenAsync();
        }
    }
}
