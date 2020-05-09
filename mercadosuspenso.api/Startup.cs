using mercadosuspenso.api.Extensions;
using mercadosuspenso.ioc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace mercadosuspenso.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBoostrap();
            services.AddProtectedControllers();
            services.AddConfiguration();
            services.AddSwagger();
            services.AddAuth();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCorsConfig();
            app.UseExceptionHandlers();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpointsConfig();
            app.UseSwaggerConfig();
        }
    }
}