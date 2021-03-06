using Microsoft.Extensions.DependencyInjection;
using QuartzNETDependencyInversionSample.Extensions;
using QuartzNETDependencyInversionSample.Listeners;
using QuartzNETDependencyInversionSample.Models;

namespace QuartzNETDependencyInversionSample
{
    public class Startup : IWorkerStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddJobSchedulerHostedService();
            services.AddQuartzNetForScheduler(options =>
            {
                options.WaitForAllJobsCompleteWhenAppCloses = false;
            });
            services.AddExceptionJobListener<DefaultExceptionListener>();

            services.AddHostedService<SampleWorkerService>();
        }
    }
}