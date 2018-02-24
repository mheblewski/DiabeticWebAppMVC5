using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ApiInfrastructure.Clients.Measurement;
using ApiInfrastructure.Models;
using AutoMapper;
using DiabeticWebAppMVC5.Models;

namespace DiabeticWebAppMVC5.Controllers
{
    public class MeasurementsController : BaseController
    {
        private readonly IMeasurementClient _measurementClient;
        private readonly IMapper _mapper;
        public MeasurementsController(IMeasurementClient measurementClient, IMapper mapper)
        {
            _measurementClient = measurementClient;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> AllMeasurements()
        {
            List<MeasurementViewModel> viewModel = null;
            var token = GetAuthorizationTokenFromSession();
            var response = await _measurementClient.TryToGetUserMeasurements(token);
            if (response.IsSuccessStatusCode)
            {
                var apiModel = await _measurementClient.GetMeasurements(response);
                viewModel = _mapper.Map<List<MeasurementApiModel>, List<MeasurementViewModel>>(apiModel);
            }
            else if (response.StatusCode == HttpStatusCode.NoContent)
            {
                ClearAndAddModelError("No measurements to show.");
            }
            else
            {
                ClearAndAddModelError("Something wrong, try agin later.");
            }

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult AddMeasurement()
        {
            var model = new MeasurementViewModel();
            return PartialView(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddMeasurement(MeasurementViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }
            var apiModel = _mapper.Map<MeasurementViewModel, MeasurementApiModel>(model);
            var token = GetAuthorizationTokenFromSession();
            var response = await _measurementClient.TryToAddMeasurement(apiModel, token);
            if (response.IsSuccessStatusCode)
            {
                return JavaScript("location.reload(true)");
            }

            return PartialView(model);
        }

        public async Task<ActionResult> DeleteMeasurement(int id)
        {
            var token = GetAuthorizationTokenFromSession();
            var response = await _measurementClient.TryToDeleteMeasurement(id, token);
            if (!response.IsSuccessStatusCode)
            {
                ClearAndAddModelError("A problem occurred, please try again later");
            }
            return RedirectToAction("AllMeasurements", "Measurements");
        }

        public string GetAuthorizationTokenFromSession()
        {
            return Session["token"].ToString();
        }
    }
}