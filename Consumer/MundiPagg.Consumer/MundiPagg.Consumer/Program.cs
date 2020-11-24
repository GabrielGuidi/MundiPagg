using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using System;
using System.Reflection;

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
                    services.AddHostedService<Worker>();
                    services.AddSingleton(serviceProvider =>
                    {
                        return new ConnectionFactory
                        {
                            HostName = "localhost",
                            UserName = "rabbitmq",
                            Password = "rabbitmq",
                            VirtualHost = "/",
                            DispatchConsumersAsync = true
                        };
                    });
                });
    }
}
