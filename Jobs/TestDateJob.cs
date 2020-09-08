using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuartzNETDependencyInversionSample.Models;
using QuartzNETDependencyInversionSample.Schedulers;

namespace QuartzNETDependencyInversionSample.Jobs
{
    public class TestDateJob : BaseJob, IDateJob
    {
        private readonly ILogger _logger;

        public TestDateJob(ILogger<TestDateJob> logger, IJobScheduler jobScheduler)
            : base(jobScheduler)
        {
            _logger = logger;
        }

        public override Task ExecuteAsync()
        {
            _logger.LogInformation("TestDateJob executed.");

            return Task.CompletedTask;
        }

        public Task<DateTime?> GetNextDateAsync()
        {
            DateTime? nextDate = DateTime.Now.AddSeconds(10);

            return Task.FromResult(nextDate);
        }
    }
}