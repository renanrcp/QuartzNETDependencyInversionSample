using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzNETDependencyInversionSample.Listeners
{
    public interface IJobExceptionListener
    {
        Task HandleExceptionAsync(Exception jobException, CancellationToken cancellationToken = default);
    }
}