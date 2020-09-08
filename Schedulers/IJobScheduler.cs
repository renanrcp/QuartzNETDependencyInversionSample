using System.Threading;
using System.Threading.Tasks;
using QuartzNETDependencyInversionSample.Models;

namespace QuartzNETDependencyInversionSample.Schedulers
{
    public interface IJobScheduler
    {
        Task StartAsync(CancellationToken cancellationToken = default);

        Task StopAsync(bool waitForCompletion, CancellationToken cancellationToken = default);

        Task ScheduleJobAsync(JobBuilder jobBuilder);

        Task<bool> JobExistsAsync(string jobName, string jobGroup);
    }
}