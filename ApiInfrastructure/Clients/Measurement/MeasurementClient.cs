using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ApiInfrastructure.Models;
using Newtonsoft.Json;

namespace ApiInfrastructure.Clients.Measurement
{
    public class MeasurementClient : BaseClient, IMeasurementClient
    {
        private readonly string measurementsUrl = "api/measurements";
        public async Task<HttpResponseMessage> TryToGetUserMeasurements(string token)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return await client.GetAsync(measurementsUrl);
            }
        }

        public async Task<List<MeasurementApiModel>> GetMeasurements(HttpResponseMessage responseMessage)
        {
            var jsonMessage = await GetJsonMessage(responseMessage);
            var measurementsList = (List<MeasurementApiModel>)JsonConvert.DeserializeObject(jsonMessage, typeof(List<MeasurementApiModel>));
            measurementsList.Reverse();

            return measurementsList;
        }

        public async Task<HttpResponseMessage> TryToAddMeasurement(MeasurementApiModel model, string token)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var myContent = JsonConvert.SerializeObject(model);
                var buffer = Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = await client.PostAsync(measurementsUrl, byteContent);
                return result;
            }
        }

        public async Task<HttpResponseMessage> TryToDeleteMeasurement(int id, string token)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAddress);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return await client.DeleteAsync(measurementsUrl + "/" + id);
            }
        }
    }
}
