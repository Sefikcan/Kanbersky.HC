using Consul;
using Kanbersky.HC.Core.Settings.Concrete.Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using Winton.Extensions.Configuration.Consul;

namespace Kanbersky.HC.Core.Helpers
{
    /// <summary>
    /// Consul Helper
    /// </summary>
    public static class ConsulHelper
    {
        /// <summary>
        /// Configure Consul Operation
        /// </summary>
        public static Action<HostBuilderContext, IConfigurationBuilder> Configure => (ctx, cb) =>
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

            ConsulSettings consulSettings = new ConsulSettings();
            config.GetSection(nameof(ConsulSettings)).Bind(consulSettings);

            string applicationName = ctx.HostingEnvironment.ApplicationName;

            //TODO: Environment bazlı işlem yaparsan aç!
            //string environmentName = ctx.HostingEnvironment.EnvironmentName;

            void ConsulConfig(ConsulClientConfiguration configuration)
            {
                configuration.Address = new Uri(consulSettings.Url);
            }

            cb.AddConsul($"{applicationName}/appsettings.json",
                source =>
                {
                    source.Optional = true;
                    source.ReloadOnChange = true; // Consul'deki veriler değişirse verilerin değişmesi için kullanılır.
                    source.ConsulConfigurationOptions = ConsulConfig; // Consul Config işlemleri için kullanırız.
                    source.PollWaitTime = TimeSpan.FromSeconds(5); // Herhangi bir key'de veri değiştirilirse ne kadar süre sonunda değişiklik yapılması için beklenilecek süre
                    source.OnLoadException = ex => // Consul yüklenirken hata olması durumunda çalışır
                    {
                        Console.WriteLine($"{ex.Exception}");
                        ex.Ignore = true;
                    };
                    source.OnWatchException = ex => // Watch işlemi sırasında hata olması durumunda çalışır
                    {
                        Console.WriteLine($"{ex.Exception}");
                        return TimeSpan.FromSeconds(2);
                    };
                });
        };
    }
}
