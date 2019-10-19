using Hbsis.Library.CrossCutting.Exceptions;
using Hbsis.Library.CrossCutting.Interop.Dto;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Hbsis.Library.CrossCutting.Helper
{
    public class HandleExceptionHelper
    {
        private readonly RequestDelegate _next;

        public HandleExceptionHelper(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleException(context, e);
            }
        }

        private static Task HandleException(HttpContext context, Exception exception)
        {
            HttpStatusCode code;
            object response = new InternalServerErrorResponseDto(exception.Message);

            switch (exception)
            {
                case UnauthorizedException _:
                    code = HttpStatusCode.Unauthorized;
                    break;

                case ForbiddenException _:
                    code = HttpStatusCode.Forbidden;
                    break;

                case EntityValidationException _:
                case VersionException _:
                    code = HttpStatusCode.BadRequest;
                    break;

                case NotFoundException _:
                    code = HttpStatusCode.NotFound;
                    break;

                default:
                    code = HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(response,
                new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() },
                    Formatting = Formatting.Indented
                }));
        }
    }
}