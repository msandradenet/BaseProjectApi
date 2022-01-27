using Infra.Interfaces;
using Infra.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.SqlClient;

namespace API.Configurations
{
    public static class DatabaseSetup
    {
        public static void AddDatabaseSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddScoped<IUnitOfWork, UnitOfWork>(x =>
            {
                var cnn = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));

                cnn.Open();

                return new UnitOfWork(cnn);
            });
        }
    }
}
