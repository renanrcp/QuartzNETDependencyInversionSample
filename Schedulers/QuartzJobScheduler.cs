using System;
using System.Threading;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl.Matchers;
using QuartzNETDependencyInversionSample.Internal;
using QuartzNETDependencyInversionSample.Listeners;
using QuartzNETDependencyInversionSample.Models;
using InternalBuilder = QuartzNETDependencyInversionSample.Models.JobBuilder;
using JobBuilder = Quartz.JobBuilder;

namespace QuartzNETDependencyInversionSample.Schedulers
{
    internal sealed class QuartzJobScheduler : IJobScheduler
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly QuartzExceptionJobListener _exceptionListener;

        public QuartzJobScheduler(ISchedulerFactory schedulerFactory, QuartzExceptionJobListener exceptionListener)
        {
            _schedulerFactory = schedulerFactory;
            _exceptionListener = exceptionListener;
        }

        private IScheduler Scheduler { get; set; }

        public Task<bool> JobExistsAsync(string jobName, string jobGroup)
            => Scheduler.CheckExists(new JobKey(jobName, jobGroup));

        public async Task ScheduleJobAsync(InternalBuilder jobBuilder)
        {
            jobBuilder.Build();

            var jobKey = new JobKey(jobBuilder.JobData.JobName, jobBuilder.JobData.JobGroup);
            var triggerKey = new TriggerKey(jobBuilder.JobData.TriggerName, jobBuilder.JobData.JobGroup);

            var data = new JobDataMap();

            data.Add("builder", jobBuilder);

            var job = JobBuilder.Create<GenericQuartzJob>()
                                .WithIdentity(jobKey)
                                .UsingJobData(data)
                                .Build();

            var triggerBuilder = TriggerBuilder.Create()
                                .WithIdentity(triggerKey);

            if (jobBuilder.JobStartDate.HasValue)
                triggerBuilder = triggerBuilder.StartAt((DateTimeOffset)jobBuilder.JobStartDate.Value);
            else
                triggerBuilder = triggerBuilder.StartNow();

            ITrigger trigger;

            if (typeof(IDateJob).IsAssignableFrom(jobBuilder.JobType) && jobBuilder.JobStartDate.HasValue)
            {
                trigger = triggerBuilder
                                    .Build();
            }
            else
            {
                trigger = triggerBuilder
                                    .WithSimpleSchedule((schedule) =>
                                    {
                                        if (jobBuilder.JobDelay.HasValue)
                                            schedule.WithInterval(jobBuilder.JobDelay.Value);

                                        if (jobBuilder.Infinite)
                                            schedule.RepeatForever();
                                    })
                                    .Build();
            }

            if (await Scheduler.CheckExists(jobKey))
            {
                await Scheduler.RescheduleJob(triggerKey, trigger);
            }
            else
            {
                await Scheduler.ScheduleJob(job, trigger);
            }
        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);

            await Scheduler.Start(cancellationToken);

            Scheduler.ListenerManager.AddJobListener(_exceptionListener, GroupMatcher<JobKey>.AnyGroup());
        }

        public Task StopAsync(bool waitForCompletion, CancellationToken cancellationToken = default)
            => Scheduler.Shutdown(waitForCompletion, cancellationToken);

        public Task CancelJobAsync(string jobName, string jobGroup)
        {
            var jobKey = new JobKey(jobName, jobGroup);

            return Scheduler.PauseJob(jobKey);
        }
    }
}