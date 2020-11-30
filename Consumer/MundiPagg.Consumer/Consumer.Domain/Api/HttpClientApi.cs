using Consumer.Domain.Api.Interfaces;
using System;
using System.Net;
using System.Net.Http;

namespace Consumer.Domain.Api
{
    public class HttpClientApi : IHttpClientApi
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string username = "sk_test_069ep0VAf4hNDlLX";
        private const string password = "";

        public HttpClientApi(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public HttpRequestMessage GetRequest(string endpoint, HttpMethod method = null, StringContent content = null)
        {
            var request = new HttpRequestMessage(method ?? HttpMethod.Get, endpoint);
            GetHeaders(request);

            if (content != null)
                request.Content = content;

            return request;
        }

        public HttpClient GetClient()
        {
            return _clientFactory.CreateClient();
        }

        public string Request(string endpoint, HttpMethod method = null, StringContent content = null)
        {
            using var client = GetClient();
            var request = GetRequest(endpoint, method, content);
            using var response = client.SendAsync(request);

            var responseJson = response.Result.Content.ReadAsStringAsync().Result;

            if (response.Result.StatusCode == HttpStatusCode.OK)
                return responseJson;

            return null;
        }

        private void GetHeaders(HttpRequestMessage request)
        {
            string encoded = Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
            
            request.Headers.Add("Accept", "application/json, text/plain, */*");
            request.Headers.Add("Accept-Language", "pt-BR,pt;q=0.9,en-US;q=0.8,en;q=0.7");
            //request.Headers.Add("Content-Type", "application/json");
            request.Headers.Add("Authorization", "Basic " + encoded);
        }
    }
}
