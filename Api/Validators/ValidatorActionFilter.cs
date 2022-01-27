using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace API.Validators
{
    public class ValidatorActionFilter : IActionFilter
    {
        private readonly ILogger<ValidatorActionFilter> _logger;

        public ValidatorActionFilter(ILogger<ValidatorActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            StringBuilder mensagens = new StringBuilder();

            if (!filterContext.ModelState.IsValid)
            {
                filterContext.ModelState.Keys
                       .Select(key =>
                       {
                           filterContext.ModelState[key].Errors.Select(x => x).ToList().ForEach(ModelError =>
                           {
                               mensagens.Append(ModelError.ErrorMessage);
                           });

                           return key;
                       }).ToList();

                var result = new BaseResponse<string>
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = mensagens.ToString().Trim()
                };

                _logger.LogError(mensagens.ToString());

                filterContext.Result = new OkObjectResult(result);
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            try
            {
                dynamic result = filterContext.Result;

                if (result != null)
                {
                    if (!string.IsNullOrEmpty(result.Value.Message))
                    {
                        string message = Convert.ToString(result.Value.Message);

                        if ((int)result.Value.StatusCode == (int)HttpStatusCode.BadRequest)
                        {
                            _logger.LogError(message);
                        }
                        else if ((int)result.Value.StatusCode == (int)HttpStatusCode.NotFound)
                        {
                            _logger.LogWarning(message);
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }
}
