using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ApiInfrastructure.Clients.Login;
using ApiInfrastructure.Models;
using AutoMapper;
using DiabeticWebAppMVC5.Models;

namespace DiabeticWebAppMVC5.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ILoginClient _loginClient;
        private readonly IMapper _mapper;
        public AccountController(ILoginClient loginClient, IMapper mapper)
        {
            _loginClient = loginClient;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }

            var apiModel = _mapper.Map<LoginViewModel, LoginApiModel>(model);
            var responseMessage = await _loginClient.TryAuthorize(apiModel);
            if (!responseMessage.IsSuccessStatusCode)
            {
                ClearAndAddModelError("The username or password is incorrect");
                return PartialView(model);
            }
            var tokenResponse = await _loginClient.GetTokenResponse(responseMessage);
            AddTokenToSession(tokenResponse.AccessToken);
            return JavaScript("window.location = '/Measurements/AllMeasurements'");
        }

        private void AddTokenToSession(string token)
        {
            Session["token"] = token;
        }
    }
}