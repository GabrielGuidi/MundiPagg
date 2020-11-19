using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using MundiPagg.AppService.OrderApplicationServices;
using MundiPagg.AppService.OrderApplicationServices.Interfaces;
using MundiPagg.Domain.CreateOrders.Interfaces;
using MundiPagg.Domain.CreateOrders.Services;
using MundiPagg.Infra.Orders.Brokers;
using MundiPagg.Infra.Orders.Repositories;
using MundiPagg.Infra.Shared;
using MundiPagg.Infra.Shared.Interfaces;
using System;
using System.IO;

namespace MundiPagg
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped(typeof(IOrderAppService), typeof(OrderAppService));
            services.AddScoped(typeof(IOrderService), typeof(OrderService));
            services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
            services.AddScoped(typeof(IOrderBroker), typeof(OrderBroker));
            services.AddScoped(typeof(IMessageBroker), typeof(MessageBroker));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "MundiPagg",
                        Version = "v1",
                        Description = "API de transformação de requisições (pagamento de cartão de crédito) e processamento assíncrono.",
                        Contact = new OpenApiContact
                        {
                            Name = "Gabriel Guidi",
                            Url = new Uri("https://github.com/GabrielGuidi/MundiPagg")
                        }
                    });

                var applicationBasePath = PlatformServices.Default.Application.ApplicationBasePath;
                var applicationName = PlatformServices.Default.Application.ApplicationName;
                var xmlDocumentPath = Path.Combine(applicationBasePath, $"{applicationName}.xml");

                if (File.Exists(xmlDocumentPath))
                {
                    c.IncludeXmlComments(xmlDocumentPath);
                }
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MundiPagg.Api V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
