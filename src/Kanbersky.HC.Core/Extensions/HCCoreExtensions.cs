using Kanbersky.HC.Core.Mappings.Abstract;
using Kanbersky.HC.Core.Mappings.Concrete.Mapster;
using Kanbersky.HC.Core.Middlewares;
using Kanbersky.HC.Core.Settings.Concrete.Consul;
using Kanbersky.HC.Core.Settings.Concrete.Swagger;
using Kanbersky.HealthChecks.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Kanbersky.HC.Core.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class HCCoreExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterCoreLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IKanberskyMapping, MapsterMapping>();

            SwaggerSettings swaggerSettings = new SwaggerSettings();
            configuration.GetSection(nameof(SwaggerSettings)).Bind(swaggerSettings);
            services.AddSingleton(swaggerSettings);

            ConsulSettings consulSettings = new ConsulSettings();
            configuration.GetSection(nameof(ConsulSettings)).Bind(consulSettings);
            services.AddSingleton(consulSettings);

            services.AddApiVersioning(cfg =>
            {
                //Header'a api version bilgisini ekler. Deprecate'ler dahil
                cfg.ReportApiVersions = true;
                cfg.ApiVersionReader = new UrlSegmentApiVersionReader();

                cfg.DefaultApiVersion = new ApiVersion(1, 0);

                //Api sürümü belirtilmezse varsayılan kullanılır.
                cfg.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddVersionedApiExplorer(cfg =>
            {
                cfg.GroupNameFormat = "'v'VVV";
                cfg.SubstituteApiVersionInUrl = true;
                cfg.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });

            services.AddSwaggerGen(c =>
            {
                var provider = services.BuildServiceProvider();
                var service = provider.GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (ApiVersionDescription desc in service.ApiVersionDescriptions)
                {
                    c.SwaggerDoc(desc.GroupName, CreateInfoForApiVersion(desc, swaggerSettings));
                }

                var xmlFile = $"{ Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.ResolveConflictingActions(apc => apc.First());
            });

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="apiVersionDescriptionProvider"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCoreLayer(this IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger(c =>
                {
                    c.RouteTemplate = "/swagger/{documentName}/swagger.json";
                });
                app.UseSwaggerUI(c =>
                {
                    foreach (ApiVersionDescription desc in apiVersionDescriptionProvider.ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", desc.GroupName.ToLowerInvariant());
                    }
                });
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();               
            });

            app.UseKanberskyHealthChecks();

            return app;
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description, SwaggerSettings swaggerSettings)
        {
            var info = new OpenApiInfo()
            {
                Title = swaggerSettings.Title,
                Version = description.ApiVersion.ToString()
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}
