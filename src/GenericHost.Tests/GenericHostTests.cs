using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Thor.GenericHost.Tests
{
    public class GenericHostTests
    {
        public async Task Sample_GenericHost()
        {
            Dictionary<string, string> configuration = new Dictionary<string, string>
            {
                {"Tracing:ApplicationId", "999"},
                {"Tracing:Level", "Warning"},
                {"Tracing:BlobStorage:ConnectionString", ""},
                {"Tracing:EventHub:ConnectionString", ""}
            };

            IHost host = new HostBuilder()
                .ConfigureHostConfiguration(builder =>
                        builder.AddInMemoryCollection(configuration))
                .BuildWithTracing();

            await host.RunAsync();
        }
    }
}
