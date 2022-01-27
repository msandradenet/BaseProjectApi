using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace API.Configurations
{
    public static class LogSetup
    {
        public static IServiceCollection AddLogSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddLogging(logging =>
            {
                logging.AddConfiguration(configuration);

                logging.AddConsole();
                logging.AddDebug();
                logging.AddFile(configuration.GetValue<string>("LogsDirectory") + configuration.GetValue<string>("Application:Name") + "/" + "ERROR-{Date}.txt", LogLevel.Error);
                logging.AddFile(configuration.GetValue<string>("LogsDirectory") + configuration.GetValue<string>("Application:Name") + "/" + "WARNING-{Date}.txt", LogLevel.Warning);
                logging.AddFile(configuration.GetValue<string>("LogsDirectory") + configuration.GetValue<string>("Application:Name") + "/" + "INFO-{Date}.txt", LogLevel.Information);
            });

            return services;
        }
    }
}