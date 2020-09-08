using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuartzNETDependencyInversionSample.Models;
using QuartzNETDependencyInversionSample.Schedulers;

namespace QuartzNETDependencyInversionSample.Jobs
{
    public class TestJob : BaseJob
    {
        private readonly ILogger _logger;

        public TestJob(ILogger<TestJob> logger, IJobScheduler jobScheduler)
            : base(jobScheduler)
        {
            _logger = logger;
        }

        public override Task ExecuteAsync()
        {
            _logger.LogInformation("TestJob executed.");

            //return CancelAsync();

            return Task.CompletedTask;
        }
    }
}