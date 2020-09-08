using System.Threading.Tasks;

namespace QuartzNETDependencyInversionSample.Models
{
    public interface IJob
    {
        JobData Data { get; set; }

        Task ExecuteAsync();

        Task CancelAsync();
    }
}