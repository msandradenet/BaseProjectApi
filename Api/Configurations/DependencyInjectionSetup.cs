using Core.Interfaces.Services;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace API.Configurations
{
    public static class DependencyInjectionSetup
    {
        public static void AddDependencyInjectionSetup(this IServiceCollection services)
        {
            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IProfissaoService, ProfissaoService>();
        }
    }
}
