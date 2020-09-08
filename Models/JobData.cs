namespace QuartzNETDependencyInversionSample.Models
{
    public class JobData
    {
        public JobData(string jobName, string triggerName, string jobGroup)
        {
            JobName = jobName;
            JobGroup = jobGroup;
            TriggerName = triggerName;
        }

        public string JobName { get; }

        public string JobGroup { get; }

        public string TriggerName { get; }
    }
}