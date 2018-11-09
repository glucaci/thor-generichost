using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Thor.Core;

namespace Thor.GenericHost
{
    /// <summary>
    /// Extension methods for Generic Host Builder
    /// </summary>
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// Configure tracing and build the host.
        /// </summary>
        public static void RunWithTracing(
            this IHostBuilder hostBuilder)
        {
            RegisterForUnhandledExceptions();

            IHost host = hostBuilder
                .ConfigureServices((context, builder) =>
                    builder.AddTracing(context.Configuration))
                .Build();   

            host
                .Services
                .GetService<HostTelemetryInitializer>()
                .Initialize();

            host.RunSafe();
        }

        private static void RegisterForUnhandledExceptions()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                Application.UnhandledException(args.ExceptionObject as Exception);

            TaskScheduler.UnobservedTaskException += (sender, args) =>
                Application.UnhandledException(args.Exception);
        }
    }
}
