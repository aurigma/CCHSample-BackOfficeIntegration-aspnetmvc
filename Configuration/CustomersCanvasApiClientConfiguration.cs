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
        Aurigma.BackOffice.IApiClientConfiguration
    {
        private CustomersCanvasOptions _ccoptions;
        private TokenService _tokenService;
        public CustomersCanvasApiClientConfiguration(IOptions<CustomersCanvasOptions> customersCanvasOptions, TokenService tokenService)
        {
            _tokenService = tokenService;
            _ccoptions = customersCanvasOptions.Value;
        }

        public string GetApiKey()
        {
            return "";
        }

        public string GetApiUrl()
        {
            return _ccoptions.ApiUrl;
        }

        public Task<string> GetAuthorizationTokenAsync()
        {
            return _tokenService.GetTokenAsync();
        }
    }
}
