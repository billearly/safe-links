using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SafeLinks.Models;
using System;
using System.Net;
using System.Net.Http;

namespace SafeLinks.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ArgumentException)
            {
                var error = new ErrorResult
                {
                    Status = 400,
                    Message = context.Exception.Message
                };

                var result = new JsonResult(error);
                result.StatusCode = (int)HttpStatusCode.BadRequest;

                context.Result = result;
                return;
            }

            if (context.Exception is HttpRequestException)
            {
                var error = new ErrorResult
                {
                    Status = 500,
                    Message = "Unable to make the request"
                };

                var result = new JsonResult(error);
                result.StatusCode = (int)HttpStatusCode.InternalServerError;

                context.Result = result;
                return;
            }

            HandleUnknownException(context);
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            // Log this

            var error = new ErrorResult
            {
                Status = 500,
                Message = "An unknown error has occured"
            };

            var result = new JsonResult(error);
            result.StatusCode = (int)HttpStatusCode.InternalServerError;

            context.Result = result;
        }
    }
}