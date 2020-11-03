using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.Extentions
{
    public static class SwaggerServiceExtantions
    {
        public static IServiceCollection AddSwaggerDocumentations(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo{ Title = "SkiCore API", Version = "v1" });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => 
                { c.SwaggerEndpoint("/swagger/v1/swagger.json", "SkiCore API v1"); });
            
            return app;
        }
    }
}