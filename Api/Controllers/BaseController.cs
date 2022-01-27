using System;
using System.Net;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.Extensions;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseController : ControllerBase
    {
        private readonly ILogger<BaseController> _logger;
        protected readonly string _currentUserID;
        protected readonly string _accesstoken;

        public BaseController(ILogger<BaseController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
        }

        [NonAction]
        public ObjectResult CreateErrorResponse<T>(string message)
        {
            return Ok(
                new BaseResponse<T>
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = message
                });
        }
        [NonAction]
        public ObjectResult CreateSuccessResponse<T>(T data, string message = "")
        {
            return Ok(
                new BaseResponse<T>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = message,
                    Result = data
                });
        }

        [NonAction]
        public ObjectResult CreateNotFoundResponse<T>(string message = "")
        {
            return Ok(
                new BaseResponse<T>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = message
                });
        }

        [NonAction]
        public ActionResult Execute<T>(Func<T> action)
        {
            try
            {
                return CreateSuccessResponse<T>(action());
            }
            catch (ArgumentException ex)
            {
                return CreateNotFoundResponse<T>(ex.Message);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse<T>(ex.Message);
            }
        }

        [NonAction]
        public ActionResult ExecuteFile<T,E>(Func<E> action) where E:FileStreamResult
        {
            try
            {
                return action();
            }
            catch (ArgumentException ex)
            {
                return CreateNotFoundResponse<T>(ex.Message);
            }
            catch (Exception ex)
            {
                return CreateErrorResponse<T>(ex.Message);
            }
        }
    }
}