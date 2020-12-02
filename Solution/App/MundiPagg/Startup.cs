using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
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
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MundiPagg
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public ILifetimeScope AutofacContainer { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "MundiPagg",
                        Version = "v1",
                        Description = "Credit card payment simulator.",
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

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<MundiContext>().As<IMundiContext>().InstancePerLifetimeScope();

            builder.RegisterType<OrderAppService>().As<IOrderAppService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderBroker>().As<IOrderBroker>().InstancePerLifetimeScope();
            builder.RegisterType<MessageBroker>().As<IMessageBroker>().InstancePerLifetimeScope();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>().InstancePerLifetimeScope();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(new CultureInfo("pt-BR")),
                SupportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("pt-BR")
                },
                SupportedUICultures = new List<CultureInfo>
                {
                    new CultureInfo("pt-BR")
                }
            });

            AutofacContainer = app.ApplicationServices.GetAutofacRoot();

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
