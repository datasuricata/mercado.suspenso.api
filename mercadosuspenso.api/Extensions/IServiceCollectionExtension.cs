using mercadosuspenso.api.Providers;
using mercadosuspenso.domain.Interfaces.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace mercadosuspenso.api.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Autenticação Bearer via JWT",
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                            },
                            new List<string>()
                        }
                    });

                c.EnableAnnotations();

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API REST +Alimento Suspenso",
                    Version = "v1",
                    Description = "Documentação de endpoints do projeto +alimento suspenso",
                    Contact = new OpenApiContact
                    {
                        Name = "Contate o desenvolvedor",
                        Url = new Uri("https://datasuricata.github.io/"),
                        Email = "lucas.moraes@datasuricata.com.br",
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Informações de licensa",
                        Url = new Uri("https://alimentocuritiba.com.br/termo")
                    }
                });

                c.CustomSchemaIds(x => x.FullName);
            });

            return services;
        }

        public static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services.AddTransient<IAuthenticationProvider, AuthenticationProvider>();

            var config = services.BuildServiceProvider().GetService<IConfiguration>();

            var key = Encoding.ASCII.GetBytes(config["SecurityKey"]);

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("stockist", policy => policy.RequireRole("STOCKIST"));
                options.AddPolicy("dispenser", policy => policy.RequireRole("DISPENSER"));
                options.AddPolicy("retailer", policy => policy.RequireRole("RETAILER"));
                options.AddPolicy("management", policy => policy.RequireRole("MANAGEMENT"));
                options.AddPolicy("reports", policy => policy.RequireRole("REPORTS"));
                options.AddPolicy("imports", policy => policy.RequireRole("IMPORTS"));
            });

            return services;
        }

        public static IServiceCollection AddProtectedControllers(this IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser()
                    .Build();

                config.Filters.Add(new AuthorizeFilter(policy));
            });

            return services;
        }

        public static IServiceCollection AddConfiguration(this IServiceCollection services)
        {
            var config = services.BuildServiceProvider().GetService<IConfiguration>();

            services.AddScoped<ISendGridClient>(x => new SendGridClient(config["SENDGRID_API_KEY"]));

            return services;
        }
    }
}