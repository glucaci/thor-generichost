using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Thor.GenericHost
{
    public static class HostBuilderExtensions
    {
        public static IHost BuildWithTracing(
            this IHostBuilder hostBuilder)
        {
            IHost host = hostBuilder
                .ConfigureServices((context, builder) =>
                    builder.AddTracing(context.Configuration))
                .Build();

            host
                .Services
                .GetService<HostTelemetryInitializer>()
                .Initialize();

            return host;
        }
    }
}
