using System;
using System.Threading;
using System.Threading.Tasks;
using QuartzNETDependencyInversionSample.Models;

namespace QuartzNETDependencyInversionSample.Listeners
{
    public interface IJobExceptionListener
    {
        Task HandleExceptionAsync(JobBuilder jobBuilder, Exception jobException, CancellationToken cancellationToken = default);
    }
}