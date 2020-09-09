using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using QuartzNETDependencyInversionSample.Models;
using InternalJob = QuartzNETDependencyInversionSample.Models.IJob;
using InternalBuilder = QuartzNETDependencyInversionSample.Models.JobBuilder;
using QuartzJob = Quartz.IJob;
using QuartzNETDependencyInversionSample.Schedulers;
using QuartzNETDependencyInversionSample.Extensions;

namespace QuartzNETDependencyInversionSample.Internal
{
    public class GenericQuartzJob : QuartzJob
    {
        private readonly IJobScheduler _jobScheduler;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public GenericQuartzJob(IJobScheduler jobScheduler, IServiceScopeFactory serviceScopeFactory)
        {
            _jobScheduler = jobScheduler;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var builder = context.GetJobBuilder();

            using var scope = _serviceScopeFactory.CreateScope();

            var job = (InternalJob)ActivatorUtilities.CreateInstance(scope.ServiceProvider, builder.JobType);

            job.Data = builder.JobData;

            if (job is IDateJob dateJob && builder.Infinite)
                return ExecuteDateJobAsync(dateJob, builder);
            else
                return ExecuteJobAsync(job);
        }

        private Task ExecuteJobAsync(InternalJob job)
            => job.ExecuteAsync();

        private async Task ExecuteDateJobAsync(IDateJob dateJob, InternalBuilder builder)
        {
            await dateJob.ExecuteAsync();

            var nextDate = await dateJob.GetNextDateAsync();

            if (!nextDate.HasValue)
            {
                await dateJob.CancelAsync();
                return;
            }

            builder = builder.WithStartDate(nextDate.Value);

            await _jobScheduler.ScheduleJobAsync(builder);
        }
    }
}