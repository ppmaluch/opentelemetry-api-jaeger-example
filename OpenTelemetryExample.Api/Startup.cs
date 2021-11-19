using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetryExample.Api.Controllers;

namespace OpenTelemetryExample.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OpenTelemetryExample.Api", Version = "v1" });
            });


            services.AddOpenTelemetryTracing(
                (builder) => builder
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddJaegerExporter(j => { j.AgentHost = "jaeger"; j.AgentPort = 6831; })
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("WeatherService"))
            );



            //services.AddOpenTelemetryTracing((sp, builder) =>
            //{
            //    builder.AddAspNetCoreInstrumentation()
            //        .AddHttpClientInstrumentation()
            //        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("App1"))

            //        .AddJaegerExporter(opts =>
            //        {
            //            opts.AgentHost = Configuration["Jaeger:AgentHost"];
            //            opts.AgentPort = Convert.ToInt32(Configuration["Jaeger:AgentPort"]);
            //        });
            //});


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OpenTelemetryExample.Api v1"));
                
            }


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
