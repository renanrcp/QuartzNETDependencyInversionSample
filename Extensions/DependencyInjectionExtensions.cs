using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Quartz;
using QuartzNETDependencyInversionSample.Internal;
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
            });

            services.TryAddTransient<GenericQuartzJob>();
            services.TryAddSingleton<IJobScheduler, QuartzJobScheduler>();

            return services;
        }
    }
}