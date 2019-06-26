using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CommonLibrary.WebSockets
{
    public static class WebSocketExtensions
    {
        public static IServiceCollection AddWebSocketManager(
            this IServiceCollection services)
        {
            services.AddTransient<WebSocketManager>();

            var exportedTypes = Assembly.GetEntryAssembly()?.ExportedTypes;
            if (exportedTypes != null)
                foreach (var type in exportedTypes)
                    if (type.GetTypeInfo().BaseType == typeof(WebSocketHandler))
                        services.AddSingleton(type);

            return services;
        }

        public static IApplicationBuilder MapWebSocketManager(
            this IApplicationBuilder app,
            PathString path,
            WebSocketHandler handler
        ) => app.Map(path, builder
                           => builder
                               .UseMiddleware<WebSocketMiddleware>(handler));
    }
}