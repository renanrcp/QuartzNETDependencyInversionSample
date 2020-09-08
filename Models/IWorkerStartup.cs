using Microsoft.Extensions.DependencyInjection;

namespace QuartzNETDependencyInversionSample.Models
{
    public interface IWorkerStartup
    {
        void ConfigureServices(IServiceCollection services);
    }
}