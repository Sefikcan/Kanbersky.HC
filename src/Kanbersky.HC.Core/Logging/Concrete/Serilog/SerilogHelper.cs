using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;

namespace Kanbersky.HC.Core.Logging.Concrete.Serilog
{
    /// <summary>
    /// 
    /// </summary>
    public class SerilogHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static Action<HostBuilderContext, LoggerConfiguration> Configure => (ctx, cfg) =>
        {
            var elasticSearchUri = ctx.Configuration.GetValue<string>("ElasticsearchSettings:Url");

            cfg.Enrich.FromLogContext()
                   .Enrich.WithMachineName()
                   .WriteTo.Console()
                   .WriteTo.Elasticsearch(
                    new ElasticsearchSinkOptions(new Uri(elasticSearchUri))
                    {
                        IndexFormat = $"microservices-{ctx.HostingEnvironment.ApplicationName?.ToLower().Replace(".", "-")}-{ctx.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-logs-{DateTime.UtcNow:yyyy-MM}",
                        AutoRegisterTemplate = true,
                        NumberOfShards = 2,
                        NumberOfReplicas = 1
                    })
                   .Enrich.WithProperty("Environment", ctx.HostingEnvironment.EnvironmentName)
                   .Enrich.WithProperty("Application", ctx.HostingEnvironment.ApplicationName)
                   .ReadFrom.Configuration(ctx.Configuration);
        };
    }
}
