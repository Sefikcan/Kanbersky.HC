using Kanbersky.HC.Catalog.Services.Extensions;
using Kanbersky.HC.Core.Extensions;
using Kanbersky.HealthChecks.Extensions;
using Kanbersky.HealthChecks.Models.Concrete;
using Kanbersky.MongoDB.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Kanbersky.HC.Catalog.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .RegisterCoreLayer(Configuration)
                .RegisterKanberskyMongoDB(Configuration)
                .AddMongoHealthCheck(new MongoDBHealthChecksModel 
                {
                    Name = "MongoDB Health",
                    FailureStatus = HealthStatus.Degraded,
                    MongoDBConnectionString = Configuration["MongoDBSettings:ConnectionStrings"]
                })
                .RegisterCatalogServiceLayer();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="apiVersionDescriptionProvider"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            app.UseCoreLayer(env, apiVersionDescriptionProvider);
        }
    }
}
