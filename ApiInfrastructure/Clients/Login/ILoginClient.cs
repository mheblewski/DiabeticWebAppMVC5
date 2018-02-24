using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ApiInfrastructure.Models;

namespace ApiInfrastructure.Clients.Login
{
    public interface ILoginClient
    {
        Task<HttpResponseMessage> TryAuthorize(LoginApiModel model);
        Task<TokenApiModel> GetTokenResponse(HttpResponseMessage response);
    }
}
