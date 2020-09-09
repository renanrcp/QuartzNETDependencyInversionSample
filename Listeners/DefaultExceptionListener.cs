using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace QuartzNETDependencyInversionSample.Listeners
{
    public class DefaultExceptionListener : IJobExceptionListener
    {
        private readonly ILogger _logger;

        public DefaultExceptionListener(ILogger<DefaultExceptionListener> logger)
        {
            _logger = logger;
        }

        public Task HandleExceptionAsync(Exception jobException, CancellationToken cancellationToken = default)
        {
            _logger.LogError(jobException, "An error occurred while executing a job.");

            return Task.CompletedTask;
        }
    }
}