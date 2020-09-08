using System;

namespace QuartzNETDependencyInversionSample.Models
{
    public class JobBuilder
    {
        public bool Infinite { get; private set; }

        public TimeSpan? JobDelay { get; private set; }

        public DateTime? JobStartDate { get; private set; }

        public JobData JobData { get; private set; }

        public IJob Job { get; private set; }

        public JobBuilder WithJob(IJob job)
        {
            Job = job;

            return this;
        }

        public JobBuilder WithInfinite(bool infinite)
        {
            Infinite = infinite;

            return this;
        }

        public JobBuilder WithDelay(TimeSpan? delay)
        {
            JobDelay = delay;

            return this;
        }

        public JobBuilder WithStartDate(DateTime? startDate)
        {
            JobStartDate = startDate;

            return this;
        }

        public void Build()
        {
            if (Job == null)
                throw new ArgumentNullException(nameof(Job));
        }
    }
}