using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MundiPagg.Worker;
using RabbitMQ.Client;

namespace MundiPagg
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<MundiPaggWorker>();
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
