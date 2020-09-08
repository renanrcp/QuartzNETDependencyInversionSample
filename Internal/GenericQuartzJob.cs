using System.Threading.Tasks;
using Quartz;
using InternalJob = QuartzNETDependencyInversionSample.Models.IJob;
namespace QuartzNETDependencyInversionSample.Internal
{
    public class GenericQuartzJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}