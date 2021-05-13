using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersCanvasSample.Configuration
{
    public class CustomersCanvasApiClientConfiguration:
        Aurigma.AssetStorage.IApiClientConfiguration,
        Aurigma.AssetProcessor.IApiClientConfiguration,
        Aurigma.StorefrontApi.IApiClientConfiguration
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
