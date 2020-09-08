using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace QuartzNETDependencyInversionSample
{
    public class SampleWorkerService : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}