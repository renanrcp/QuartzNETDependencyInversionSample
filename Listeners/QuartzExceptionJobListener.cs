using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Quartz;
using QuartzNETDependencyInversionSample.Extensions;
using InternalBuilder = QuartzNETDependencyInversionSample.Models.JobBuilder;

namespace QuartzNETDependencyInversionSample.Listeners
{
    internal sealed class QuartzExceptionJobListener : IJobListener
    {
        private readonly IEnumerable<IJobExceptionListener> _expceptionListeners;

        private const string NAME = "Quartz Exception Job Listener";

        public QuartzExceptionJobListener(IEnumerable<IJobExceptionListener> expceptionListeners)
        {
            _expceptionListeners = expceptionListeners;
        }

        public string Name => NAME;

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
            => Task.CompletedTask;

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
            => Task.CompletedTask;

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            var exception = jobException?.InnerException ?? jobException;

            if (exception == null)
                return Task.CompletedTask;

            var builder = context.GetJobBuilder();

            var tasks = _expceptionListeners
                            .Select(a => a.HandleExceptionAsync(builder, exception, cancellationToken))
                            .ToList();

            return Task.WhenAll(tasks);
        }
    }
}