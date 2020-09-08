using System.Threading;
using System.Threading.Tasks;
using Quartz;
using QuartzNETDependencyInversionSample.Models;
using InternalBuilder = QuartzNETDependencyInversionSample.Models.JobBuilder;

namespace QuartzNETDependencyInversionSample.Schedulers
{
    internal sealed class QuartzJobScheduler : IJobScheduler
    {
        private readonly ISchedulerFactory _schedulerFactory;

        private IScheduler Scheduler { get; set; }

        public QuartzJobScheduler(ISchedulerFactory schedulerFactory)
        {
            _schedulerFactory = schedulerFactory;
        }

        public Task<bool> JobExistsAsync(string jobName, string jobGroup)
            => Scheduler.CheckExists(new JobKey(jobName, jobGroup));

        public async Task ScheduleJobAsync(InternalBuilder jobBuilder)
        {
            jobBuilder.Build();

            var jobKey = new JobKey(jobBuilder.JobData.JobName, jobBuilder.JobData.JobGroup);

            await Task.Delay(0);
        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);

            await Scheduler.Start(cancellationToken);
        }

        public Task StopAsync(bool waitForCompletion, CancellationToken cancellationToken = default)
            => Scheduler.Shutdown(waitForCompletion, cancellationToken);
    }
}