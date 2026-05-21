using System.Net;
using System.Text.Json;
using TaskService.Domain.Exceptions;

namespace TaskService.Api.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex switch
                {
                    NoTienePermisosException => (int)HttpStatusCode.Forbidden,
                    TaskNoEncontradaException => (int)HttpStatusCode.NotFound,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                var json = JsonSerializer.Serialize(new { statusCode = context.Response.StatusCode, message = ex.Message });

                await context.Response.WriteAsync(json);
            }
            ;
        }
    }
}
