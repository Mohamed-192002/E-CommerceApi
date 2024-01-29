using E_Commerce.API.Errors;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace E_Commerce.API.Extensions
{
    public static class ApiRegistration
    {
        public static IServiceCollection AddApiRegistration(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errorResponse = new ApiValidationErrorsResponse
                    {
                        Errors = context.ModelState
                                        .Where(x => x.Value.Errors.Count > 0)
                                        .SelectMany(x => x.Value.Errors)
                                        .Select(x => x.ErrorMessage)
                                        .ToArray(),
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());




            return services;
        }
    }
}
