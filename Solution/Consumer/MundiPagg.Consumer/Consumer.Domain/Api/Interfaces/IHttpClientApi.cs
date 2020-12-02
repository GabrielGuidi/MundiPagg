using System.Net.Http;

namespace Consumer.Domain.Api.Interfaces
{
    public interface IHttpClientApi
    {
        public HttpClient GetClient();
        public HttpRequestMessage GetRequest(string endpoint, HttpMethod method = null, StringContent content = null);
        public string Request(string endpoint, HttpMethod method = null, StringContent content = null);
    }
}
