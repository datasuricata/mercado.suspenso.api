﻿using mercadosuspenso.domain.Interfaces.Services;
using mercadosuspenso.orm;
using mercadosuspenso.orm.Repository;
using mercadosuspenso.service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace mercadosuspenso.ioc
{
    public static class Bootstrap
    {
        public static void AddBoostrap(this IServiceCollection services)
        {
            var config = services.BuildServiceProvider().GetService<IConfiguration>();

            services.AddDbContext<MercadoDbContext>(
               options =>
               options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient<IParticipanteService, ParticipanteService>();
        }
    }
}