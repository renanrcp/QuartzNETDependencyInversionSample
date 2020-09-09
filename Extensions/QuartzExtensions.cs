using Quartz;
using InternalBuilder = QuartzNETDependencyInversionSample.Models.JobBuilder;

namespace QuartzNETDependencyInversionSample.Extensions
{
    internal static class QuartzExtensions
    {
        public static InternalBuilder GetJobBuilder(this IJobExecutionContext jobExecutionContext)
            => (InternalBuilder)jobExecutionContext.JobDetail.JobDataMap["builder"];
    }
}