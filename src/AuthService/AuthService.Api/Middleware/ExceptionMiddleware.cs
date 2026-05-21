using AuthService.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace AuthService.Api.Middleware
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
                    UsuarioRegistradoException => (int)HttpStatusCode.Conflict,
                    UsuarioInexistenteException => (int)HttpStatusCode.NotFound,
                    CredencialesInvalidasException => (int)HttpStatusCode.Unauthorized,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                var json = JsonSerializer.Serialize(new { statusCode = context.Response.StatusCode, message = ex.Message });

                await context.Response.WriteAsync(json);
            };
        }
    }
}
