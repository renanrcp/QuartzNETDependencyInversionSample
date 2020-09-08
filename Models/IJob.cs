using System.Threading.Tasks;

namespace QuartzNETDependencyInversionSample.Models
{
    public interface IJob
    {
        Task ExecuteAsync(JobData data);

        Task CancelAsync();
    }
}