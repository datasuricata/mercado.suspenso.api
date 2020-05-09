using mercadosuspenso.api.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace mercadosuspenso.api.Extensions
{
    public static class IApplicationBuilderExtension
    {
        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Alimento Suspenso API - V1"); });

            return app;
        }

        public static IApplicationBuilder UseEndpointsConfig(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }

        public static IApplicationBuilder UseCorsConfig(this IApplicationBuilder app)
        {
            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });

            return app;
        }

        public static IApplicationBuilder UseExceptionHandlers(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandler>();
        }
    }
}