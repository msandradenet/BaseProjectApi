using API.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Configurations
{
    public static class ActionFilterSetup
    {
        public static void AddActionFilter(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(x => x.SuppressModelStateInvalidFilter = true);

            services.AddMvc(opt =>
            {
                opt.Filters.Add(typeof(ValidatorActionFilter));
            })
            .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>());
        }
    }
}
