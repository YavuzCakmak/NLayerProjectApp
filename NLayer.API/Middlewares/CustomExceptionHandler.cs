using Microsoft.AspNetCore.Diagnostics;
using NLayer.Core.Dtos;
using NLayer.Service.Exceptions;
using System.Net;
using System.Text.Json;

namespace NLayer.API.Filters.Middlewares
{
    public static class CustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => HttpStatusCode.BadRequest,
                        NotFoundException => HttpStatusCode.NotFound,
                        _ => HttpStatusCode.InternalServerError
                    };
                    context.Response.StatusCode = (int)statusCode;
                    var response = CustomResponseDto<NoContentDto>.Fail(exceptionFeature.Error.Message, (int)statusCode);
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });
            });
        }
    }
}
