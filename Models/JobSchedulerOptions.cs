using Microsoft.Extensions.Options;

namespace QuartzNETDependencyInversionSample.Models
{
    public class JobSchedulerOptions : IOptions<JobSchedulerOptions>
    {
        public bool WaitForAllJobsCompleteWhenAppCloses { get; set; }

        JobSchedulerOptions IOptions<JobSchedulerOptions>.Value => this;
    }
}