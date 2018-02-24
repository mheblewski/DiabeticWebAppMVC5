using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiInfrastructure.Clients
{
    public class BaseClient
    {
        protected readonly string BaseAddress = "http://www.diabeticwebapp.pl";
        protected async Task<string> GetJsonMessage(HttpResponseMessage response)
        {
            string jsonMessage;
            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                jsonMessage = new StreamReader(responseStream).ReadToEnd();
            }

            return jsonMessage;
        }
    }
}
