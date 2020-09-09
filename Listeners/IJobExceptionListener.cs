using System;
using System.Threading.Tasks;

namespace QuartzNETDependencyInversionSample.Listeners
{
    public interface IJobExceptionListener
    {
        string Name { get; }

        Task HandleExceptionAsync(Exception jobException);
    }
}