using mercadosuspenso.domain.Dtos;
using mercadosuspenso.domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace mercadosuspenso.api.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var isDomainException = ex is DomainException;

                var statusCode = isDomainException ? 400 : 500;

                var message = isDomainException ? ex.Message : "Erro interno, contate o suporte";

                var result = JsonConvert.SerializeObject
                (
                    new ProblemDto { StatusCode = statusCode, Message = message }
                );

                context.Response.ContentType = "application/json";

                context.Response.StatusCode = statusCode;

                await context.Response.WriteAsync(result);
            }
        }
    }
}