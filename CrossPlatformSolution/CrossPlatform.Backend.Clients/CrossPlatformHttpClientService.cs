using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatform.Backend.Clients
{
    public sealed class CrossPlatformHttpClientService
    {
        private static Lazy<CrossPlatformHttpClientService> _Lazy = new Lazy<CrossPlatformHttpClientService>(() => new CrossPlatformHttpClientService());

        public static CrossPlatformHttpClientService Current { get { return _Lazy.Value; } }

        private CrossPlatformHttpClientService()
        {
            this._HttpClient = new HttpClient();
            this._HttpClient.BaseAddress = new Uri("http://xamarin-api.azurewebsites.net");
        }

        private readonly HttpClient _HttpClient;

        internal async Task<T> GetAsync<T>(string requestUri)
        {
            using (var _response = await this._HttpClient.GetAsync(requestUri))
            {
                if (!_response.IsSuccessStatusCode)
                {
                    if (_response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        throw new InvalidOperationException("Acesso negado, você precisa estar autenticado para realizar essa requisição.");

                    throw new Exception("Algo de errado não deu certo.");
                }

                var _result = await _response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(_result);
            }
        }

        public async Task Autenticar()
        {
            try
            {
                if (this._HttpClient.DefaultRequestHeaders.Authorization == null)
                {
                    var _args = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("username", "jefferson@balivo.com.br"),
                        new KeyValuePair<string, string>("password", "123@Mudar"),
                    };

                    using (var _response = await this._HttpClient.PostAsync("token", new FormUrlEncodedContent(_args)))
                    {
                        if (!_response.IsSuccessStatusCode)
                            throw new InvalidOperationException("Verifique os dados informados ou sua conexão com a internet");

                        var _result = await _response.Content.ReadAsStringAsync();

                        var _tokenResult = JsonConvert.DeserializeObject<TokenResult>(_result);

                        this._HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_tokenResult.token_type, _tokenResult.access_token);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        class TokenResult
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            //public string expires_in": 1209599,
            //public string userName": "jefferson@balivo.com.br",
            //public string .issued": "Wed, 02 Nov 2016 21:06:56 GMT",
            //public string .expires": "Wed, 16 Nov 2016 21:06:56 GMT"
        }
    }
}
