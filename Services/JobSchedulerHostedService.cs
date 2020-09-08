using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using QuartzNETDependencyInversionSample.Models;
using QuartzNETDependencyInversionSample.Schedulers;

namespace QuartzNETDependencyInversionSample.Services
{
    public class JobSchedulerHostedService : IHostedService
    {
        private readonly IJobScheduler _jobScheduler;
        private readonly JobSchedulerOptions _options;

        public JobSchedulerHostedService(IJobScheduler jobScheduler, IOptions<JobSchedulerOptions> options)
        {
            _jobScheduler = jobScheduler;
            _options = options.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
            => _jobScheduler.StartAsync(cancellationToken);

        public Task StopAsync(CancellationToken cancellationToken)
            => _jobScheduler.StopAsync(_options.WaitForAllJobsCompleteWhenAppCloses, cancellationToken);
    }
}