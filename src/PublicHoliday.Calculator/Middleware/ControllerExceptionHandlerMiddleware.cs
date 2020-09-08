using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PublicHoliday.Calculator.Middleware
{
    public class ControllerExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ControllerExceptionHandlerMiddleware> _logger;

        public ControllerExceptionHandlerMiddleware(RequestDelegate next, ILogger<ControllerExceptionHandlerMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                // HTTP 200
                await _next(context);
            }
            catch (Exception ex)
            {
                // NOT SUCCESS HTTP CODES              
                await HandleExceptionAsync(context, ex, _logger);
            }
        }


        public static Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<ControllerExceptionHandlerMiddleware> logger)
        {
            // default is HTTP 500
            var statusCode = HttpStatusCode.InternalServerError;

            switch (ex)
            {
                case ArgumentException err:
                    logger.LogWarning(default(EventId), err, "ControllerExceptionHandlerMiddleware: ArgumentException", context);
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                case KeyNotFoundException err:
                    logger.LogWarning(default(EventId), err, "ControllerExceptionHandlerMiddleware: KeyNotFoundException", context);
                    statusCode = HttpStatusCode.NotFound;
                    break;
                default:
                    logger.LogError(default(EventId), ex, "ControllerExceptionHandlerMiddleware: Exception (Unhandled/HTTP 5xx)", context);
                    break;
            }

            var result = JsonConvert.SerializeObject(new { error = ex.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
