using BoilerPlate.Utils;
using Microsoft.AspNetCore.Builder;

namespace BoilerPlate.Bootstrapper
{
    public static class SwaggerConfigurationExtension
    {
        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app,
            ApplicationSettings applicationSettings)
        {

            if (!applicationSettings.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BoilerPlate API V1");
                });
            }

            return app;
        }
    }
}
