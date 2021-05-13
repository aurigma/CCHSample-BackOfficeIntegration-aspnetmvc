using IdentityModel.Client;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CustomersCanvasSample
{
    class TokenInfo
    {
        public string AccessToken { get; set; }
        public DateTime ExpiryTime { get; set; }
    }

    public class TokenService
    {
        // TokenService will be used as a singleton, so we need to
        // syncronize an access to the token stored in the service.
        private readonly object _lockObject = new object();

        private readonly HttpClient _client;
        private TokenInfo _tokenInfo;

        private CustomersCanvasOptions _options;

        public TokenService(IHttpClientFactory clientFactory, IOptions<CustomersCanvasOptions> customersCanvasOptions)
        {
            _client = clientFactory.CreateClient();
            _options = customersCanvasOptions.Value;
        }

        public async Task<string> GetTokenAsync()
        {
            lock (_lockObject)
            {
                if (_tokenInfo != null && _tokenInfo.ExpiryTime >= DateTime.UtcNow)
                {
                    return _tokenInfo.AccessToken;
                }

                _tokenInfo = null;
            }

            var tokenResponse =
                await _client.RequestClientCredentialsTokenAsync(
                    new ClientCredentialsTokenRequest
                    {
                        // A token endpoint in the IdentityProvider (BackOffice token URL)
                        Address = $"{_options.IdentityProviderUrl}/connect/token",

                        // Client Id and Secret pair - you can register them in your 
                        // BackOffice tenant control panel. 
                        // NOTE: Be sure to provide all necessary scopes for this client!
                        ClientId = _options.ClientId,
                        ClientSecret = _options.ClientSecret
                    });

            if (tokenResponse.IsError)
            {
                throw new Exception("Could not retrieve token.");
            }

            lock (_lockObject)
            {
                if (_tokenInfo == null)
                {
                    _tokenInfo = new TokenInfo()
                    {
                        AccessToken = tokenResponse.AccessToken,
                        ExpiryTime = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn),
                    };
                }
            }

            return tokenResponse.AccessToken;
        }
    }
}