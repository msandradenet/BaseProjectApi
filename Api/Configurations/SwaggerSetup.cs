using Domain.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace API.Configurations
{
    public static class SwaggerSetup
    {
        public static IServiceCollection AddSwaggerSetup(this IServiceCollection services, IConfiguration _config)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });

            var _versions = _config.GetSection("Application:Versions").Get<IList<Versions>>();
            var _name = _config.GetSection("Application:Name").Value;
            var _email = _config.GetSection("Application:Email").Value;
            var _url = _config.GetSection("Application:Url").Value;
            var _descricao = _config.GetSection("Application:Description").Value;

            services.AddSwaggerGen(s =>
            {
                var contact = new OpenApiContact { Name = _name, Email = _email, Url = new Uri(_url) };                

                foreach (var item in _versions)
                {
                    s.SwaggerDoc(item.Version, new OpenApiInfo
                    {
                        Version = item.Version,
                        Title = _name,
                        Description = _descricao,
                        Contact = contact
                    });
                }

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });

                s.OperationFilter<RemoveVersionFromParameter>();
                s.DocumentFilter<ReplaceVersionWithExactValueInPath>();

                s.DocInclusionPredicate((version, desc) =>
                {
                    if (!desc.TryGetMethodInfo(out MethodInfo methodInfo))
                        return false;

                    var versions = methodInfo.DeclaringType
                    .GetCustomAttributes(true)
                    .OfType<ApiVersionAttribute>()
                    .SelectMany(attr => attr.Versions);

                    var maps = methodInfo
                    .GetCustomAttributes(true)
                    .OfType<MapToApiVersionAttribute>()
                    .SelectMany(attr => attr.Versions)
                    .ToArray();

                    return versions.Any(v => $"v{v}" == version)
                    && (!maps.Any() || maps.Any(v => $"v{v}" == version));
                });
            });

            return services;
        }

        public static void UseSwaggerSetup(this IApplicationBuilder app, IConfiguration configuration)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                var versions = configuration.GetSection("Application:Versions").Get<IList<Versions>>();

                foreach (var item in versions)
                {
                    s.SwaggerEndpoint(string.Format("{0}/swagger.json", item.Version), string.Format("API versão: {0}", item.Description));
                }

                //s.RoutePrefix = string.Empty;
            });
        }
    }

    public class RemoveVersionFromParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var versionParameter = operation.Parameters.Single(p => p.Name == "version");
            operation.Parameters.Remove(versionParameter);
        }
    }

    public class ReplaceVersionWithExactValueInPath : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var toReplaceWith = new OpenApiPaths();

            foreach (var (key, value) in swaggerDoc.Paths)
            {
                toReplaceWith.Add(key.Replace("v{version}", swaggerDoc.Info.Version, StringComparison.InvariantCulture), value);
            }

            swaggerDoc.Paths = toReplaceWith;
        }
    }
}
