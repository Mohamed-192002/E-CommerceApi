using System.Reflection;
using ECommerce.Infrastructure;
using ECommerce.Api.AutoMapper;
using Microsoft.Extensions.FileProviders;
using System.IO;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using E_Commerce.API.Middleware;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.API.Errors;
using E_Commerce.API.Extensions;

namespace E_Commerce.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddApiRegistration();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            // Add Authorization To Swagger 
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                     {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                     });
            });
            // Configure Redis
            builder.Services.AddSingleton<IConnectionMultiplexer>(i =>
            {
                var configure = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configure);
            });
            builder.Services.InfrastructureConfigration(builder.Configuration);

            builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")
                ));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseStatusCodePagesWithReExecute("/Erorrs/{0}");

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();
            InfrastructureRegistration.InfrastructureConfigrationMiddleWare(app);
            app.Run();
        }
    }
}
