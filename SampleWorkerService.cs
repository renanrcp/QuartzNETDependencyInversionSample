using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using QuartzNETDependencyInversionSample.Jobs;
using QuartzNETDependencyInversionSample.Models;
using QuartzNETDependencyInversionSample.Schedulers;

namespace QuartzNETDependencyInversionSample
{
    public class SampleWorkerService : BackgroundService
    {
        private readonly IJobScheduler _jobScheduler;

        public SampleWorkerService(IJobScheduler jobScheduler)
        {
            _jobScheduler = jobScheduler;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var jobData = new JobData("testJob1", "testJobTrigger1", "test");

            var jobBuilder = new JobBuilder()
                                    .WithInfinite(true)
                                    .WithStartDate(DateTime.Now.AddSeconds(10))
                                    .WithDelay(TimeSpan.FromSeconds(10))
                                    .WithJob<TestJob>()
                                    .WithData(jobData);

            return _jobScheduler.ScheduleJobAsync(jobBuilder);
        }
    }
}