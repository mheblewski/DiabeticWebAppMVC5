using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ApiInfrastructure.Models;

namespace ApiInfrastructure.Clients.Measurement
{
    public interface IMeasurementClient
    {
        Task<HttpResponseMessage> TryToGetUserMeasurements(string token);
        Task<List<MeasurementApiModel>> GetMeasurements(HttpResponseMessage responseMessage);
        Task<HttpResponseMessage> TryToAddMeasurement(MeasurementApiModel model, string token);
        Task<HttpResponseMessage> TryToDeleteMeasurement(int id, string token);
    }
}
