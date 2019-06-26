using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.RabbitMQ
{
    public static class RabbitMQExtensions
    {
        public static IServiceCollection AddRabbitMQ(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = new RabbitMQOptions();
            configuration.GetSection("RabbitMQ")
                .Bind(options);

            // Does not require dispose
            var endpointConfiguration = new EndpointConfiguration(
                options.Uri,
                options.InitialName);

            services
                .AddSingleton(endpointConfiguration);

            return services;
        }
    }
}