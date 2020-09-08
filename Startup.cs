using Microsoft.Extensions.DependencyInjection;
using QuartzNETDependencyInversionSample.Models;

namespace QuartzNETDependencyInversionSample
{
    public class Startup : IWorkerStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }
    }
}