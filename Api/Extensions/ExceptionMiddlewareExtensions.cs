using System.Net;
using Domain.Entities;
using Infrastructure.Models;
using Infrastructure.Models.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var statusCode = HttpStatusCode.InternalServerError;
                        var message = "Internal Server Error";
                        
                        if (contextFeature.Error.GetType().BaseType == typeof(CustomException))
                        {
                            statusCode = ((contextFeature.Error as CustomException)!).StatusCode;
                            message = contextFeature.Error.Message;
                        }
                        // logger.LogError($"Something went wrong: {contextFeature.Error}");

                        
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = (int)statusCode,
                            Message = message
                        }.ToString());
                    }
                });
            });
        }
    }
}