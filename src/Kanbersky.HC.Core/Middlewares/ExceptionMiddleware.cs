using Kanbersky.HC.Core.Logging.Models;
using Kanbersky.HC.Core.Results.Exceptions.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kanbersky.HC.Core.Middlewares
{
    /// <summary>
    /// 
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="next"></param>
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                await ThrowError(context, ex, statusCode: StatusCodes.Status404NotFound);
            }
            catch (BadRequestException ex)
            {
                await ThrowError(context, ex, statusCode: StatusCodes.Status400BadRequest);
            }
            catch (NotImplementedException ex)
            {
                await ThrowError(context, ex, statusCode: StatusCodes.Status501NotImplemented);
            }
            catch (Exception ex)
            {
                await ThrowError(context, ex, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        private static async Task ThrowError(HttpContext context, Exception ex, string contentType = "application/json", int statusCode = StatusCodes.Status500InternalServerError)
        {
            var logModel = new LoggerModel
            {
                ResponseStatusCode = statusCode,
                InnerException = ex.Message
            };

            var responseBody = JsonSerializer.Serialize(logModel, new JsonSerializerOptions
            {
                IgnoreNullValues = true
            });

            context.Response.Clear();
            context.Response.ContentType = contentType;
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(responseBody, Encoding.UTF8);
        }
    }
}
