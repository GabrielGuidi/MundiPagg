using Consumer.Domain.Api;
using Consumer.Domain.Api.Interfaces;
using Consumer.Domain.Api.Orders;
using Consumer.Domain.Orders.Interfaces;
using Consumer.Domain.Orders.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace MundiPagg.Consumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClient();
                    services.AddSingleton<IOrderApi, OrderApi>();
                    services.AddSingleton<IOrderService, OrderService>();

                    services.AddHostedService<Worker>();
                    services.AddSingleton<IHttpClientApi, HttpClientApi>();
                    services.AddSingleton(serviceProvider =>
                    {
                        return new ConnectionFactory
                        {
                            HostName = "rabbitmq",
                            UserName = "rabbitmq",
                            Password = "rabbitmq",
                            VirtualHost = "/",
                            DispatchConsumersAsync = true
                        };
                    });
                });
    }
}
