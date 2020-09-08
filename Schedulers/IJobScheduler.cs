using System.Threading.Tasks;

namespace QuartzNETDependencyInversionSample.Schedulers
{
    public interface IJobScheduler
    {
        Task ScheduleJobAsync();

        Task JobExistsAsync(string jobName, string jobGroup);
    }
}