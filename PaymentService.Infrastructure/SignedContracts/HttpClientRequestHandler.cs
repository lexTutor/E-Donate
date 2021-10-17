using Newtonsoft.Json;
using PaymentService.Application.Contracts;
using PaymentService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.SignedContracts
{
    public class HttpClientRequestHandler : IRequestHandler
    {
        private readonly HttpClient _client;

        public HttpClientRequestHandler(HttpClient client)
        {
            _client = client;
            _client.DefaultRequestHeaders.Accept.Clear();
        }

        public async Task<TRes> GetAsync<TRes>(string url, string authorization = null)
        {
            if (!string.IsNullOrWhiteSpace(authorization))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + authorization);

         HttpResponseMessage result = await _client.GetAsync(new Uri(url));

            string content = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TRes>(content);
        }

        public async Task<TRes> SendAsync<TReq, TRes>(TReq requestData, string url, string authorization = null)
        {
            if (!string.IsNullOrWhiteSpace(authorization))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + authorization);

         string serializedModel = JsonConvert.SerializeObject(requestData);

            var content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(new Uri(url), content);

            string responseContent = await response.Content.ReadAsStringAsync();

            TRes result = JsonConvert.DeserializeObject<TRes>(responseContent);

            return result;
        }
    }
}
