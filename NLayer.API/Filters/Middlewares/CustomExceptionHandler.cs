﻿using Microsoft.AspNetCore.Diagnostics;
using NLayer.Core.Dtos;
using NLayer.Service.Exceptions;
using System.Text.Json;

namespace NLayer.API.Filters.Middlewares
{
    public static class CustomExceptionHandler
    {
        public static void UserCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => 400,
                        _ => 500
                    };
                    context.Response.StatusCode = statusCode;
                    var response = CustomResponseDto<NoContentDto>.Fail(exceptionFeature.Error.Message, statusCode);
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });
            });
        }
    }
}