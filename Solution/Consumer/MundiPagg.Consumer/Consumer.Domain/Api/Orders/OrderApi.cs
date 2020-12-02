using Consumer.Domain.Api.Interfaces;
using Consumer.Domain.Orders.Interfaces;
using System.Net.Http;
using System.Text;

namespace Consumer.Domain.Api.Orders
{
    public class OrderApi : IOrderApi
    {
        private readonly IHttpClientApi _httpClientApi;

        private readonly string _criar_pedido = "https://api.mundipagg.com/core/v1/orders/";

        public OrderApi(IHttpClientApi httpClientaApi)
        {
            _httpClientApi = httpClientaApi;
        }

        public string CreateOrder(string message)
        {
            using var messageJson = new StringContent(message, Encoding.UTF8, "application/json");

            var orderResponse = _httpClientApi.Request(_criar_pedido, HttpMethod.Post, messageJson);

            return orderResponse;
        }
    }
}
