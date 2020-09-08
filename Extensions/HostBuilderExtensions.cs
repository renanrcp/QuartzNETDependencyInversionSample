using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuartzNETDependencyInversionSample.Internal;
using QuartzNETDependencyInversionSample.Models;

namespace QuartzNETDependencyInversionSample.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseStartup<T>(this IHostBuilder hostBuilder)
            where T : class, IWorkerStartup
        {
            return hostBuilder.ConfigureServices((context, services) =>
            {
                var provider = new HostBuilderServiceProvider(context.Configuration, context.HostingEnvironment);

                var startup = ActivatorUtilities.CreateInstance<T>(provider);

                startup.ConfigureServices(services);
            });
        }
    }
}