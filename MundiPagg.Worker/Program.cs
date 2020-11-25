using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace MundiPagg.Worker
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
                    services.AddHostedService<MundiPaggWorker>();
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
