namespace QuartzNETDependencyInversionSample.Models
{
    public class JobData
    {
        public JobData(string jobName, string jobGroup)
        {
            JobName = jobName;
            JobGroup = jobGroup;
        }

        public string JobName { get; }

        public string JobGroup { get; }
    }
}