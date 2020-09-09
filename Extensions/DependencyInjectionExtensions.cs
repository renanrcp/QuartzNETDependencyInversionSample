using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Quartz;
using QuartzNETDependencyInversionSample.Internal;
using QuartzNETDependencyInversionSample.Listeners;
using QuartzNETDependencyInversionSample.Models;
using QuartzNETDependencyInversionSample.Schedulers;
using QuartzNETDependencyInversionSample.Services;

namespace QuartzNETDependencyInversionSample.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddJobSchedulerHostedService(this IServiceCollection services)
            => services.AddHostedService<JobSchedulerHostedService>();

        public static IServiceCollection AddQuartzNetForScheduler(this IServiceCollection services, Action<JobSchedulerOptions> options = null)
        {
            if (options != null)
                services.Configure(options);
            else
                services.AddOptions<JobSchedulerOptions>();

            services.AddSingleton<JobSchedulerOptions>(sp =>
            {
                return sp.GetRequiredService<IOptions<JobSchedulerOptions>>().Value;
            });

            services.AddQuartz(quartzOptions =>
            {
                quartzOptions.UseMicrosoftDependencyInjectionJobFactory(jobOptions =>
                {
                    jobOptions.AllowDefaultConstructor = false;
                });

                quartzOptions.UseSimpleTypeLoader();
                quartzOptions.UseInMemoryStore();
                quartzOptions.UseDefaultThreadPool(tp =>
                {
                    tp.MaxConcurrency = 9000;
                });
            });

            services.TryAddTransient<GenericQuartzJob>();
            services.TryAddSingleton<IJobScheduler, QuartzJobScheduler>();
            services.TryAddSingleton<QuartzExceptionJobListener>();

            return services;
        }
    }
}