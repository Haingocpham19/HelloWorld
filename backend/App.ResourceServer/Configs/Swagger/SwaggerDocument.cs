using Microsoft.Extensions.DependencyInjection;

namespace Extension.Web.Configs.Swagger
{
    public static class SwaggerDocument
    {
        /// <summary>
        /// Configure project template swagger document.
        /// </summary>
        public static IServiceCollection AddSwaggerDocument(this IServiceCollection services)
        {
            return services
                .AddSwaggerDocument(config =>
                {
                    config.PostProcess = document =>
                    {
                        document.Info.Version = "v1";
                        document.Info.Title = "Project Template API";
                        document.Info.Contact = new NSwag.OpenApiContact
                        {
                            Name = "Devices Software Experiences"
                        };
                    };
                });
        }
    }
}
