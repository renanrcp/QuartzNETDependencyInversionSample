using System.Threading.Tasks;
using QuartzNETDependencyInversionSample.Models;

namespace QuartzNETDependencyInversionSample.Schedulers
{
    public interface IJobScheduler
    {
        Task ScheduleJobAsync(JobBuilder jobBuilder);

        Task JobExistsAsync(string jobName, string jobGroup);
    }
}