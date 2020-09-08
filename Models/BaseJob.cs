using System.Threading.Tasks;
using QuartzNETDependencyInversionSample.Schedulers;

namespace QuartzNETDependencyInversionSample.Models
{
    public abstract class BaseJob : IJob
    {
        private readonly IJobScheduler _jobScheduler;

        public virtual JobData Data { get; set; }

        public abstract Task ExecuteAsync();

        public virtual Task CancelAsync()
            => _jobScheduler.CancelJobAsync(Data.JobName, Data.JobGroup);
    }
}