using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ApiInfrastructure.Models;
using Newtonsoft.Json;

namespace ApiInfrastructure.Clients.Login
{
    public class LoginClient : BaseClient, ILoginClient
    {
        private readonly string _tokenUrl = "token";

        public async Task<HttpResponseMessage> TryAuthorize(LoginApiModel model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAddress);
                var dictionary = CreateDictionaryFromLoginModelAndAddGrantType(model, "password");
                var content = new FormUrlEncodedContent(dictionary);
                var response = await client.PostAsync(_tokenUrl, content);
                return response;
            }
        }

        public async Task<TokenApiModel> GetTokenResponse(HttpResponseMessage response)
        {
            var jsonMessage = await GetJsonMessage(response);

            TokenApiModel tokenResponse = (TokenApiModel)JsonConvert.DeserializeObject
                (jsonMessage, typeof(TokenApiModel));
            return tokenResponse;
        }

        private IDictionary<string, string> CreateDictionaryFromLoginModelAndAddGrantType
            (LoginApiModel model, string grantType)
        {
            var dictionary = model.AsDictionary();
            dictionary.Add("grant_type", grantType);
            return dictionary;
        }
    }
}
