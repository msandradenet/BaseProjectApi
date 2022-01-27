using API.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API
{
    public class StartupDevelopment : Startup
    {
        public StartupDevelopment(IConfiguration configuration) : base(configuration)
        { }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddSwaggerSetup(Configuration);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);
           
            app.UseSwaggerSetup(Configuration);
        }
    }
}
