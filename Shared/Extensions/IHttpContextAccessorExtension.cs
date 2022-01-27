using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Shared.Extensions
{
    public static class IHttpContextAccessorExtension
    {
        public static Method? GetMethod(this IHttpContextAccessor httpContextAccessor)
        {
            string method = httpContextAccessor?.HttpContext?.Request.Method.ToUpper();

            Method.TryParse(method, out Method result);

            return result;
        }
    }
}
