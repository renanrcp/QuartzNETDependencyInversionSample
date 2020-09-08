using System;

namespace QuartzNETDependencyInversionSample.Models
{
    public class JobBuilder
    {
        public bool Infinite { get; private set; }

        public TimeSpan? JobDelay { get; private set; }

        public DateTime? JobStartDate { get; private set; }

        public JobData JobData { get; private set; }

        public Type JobType { get; private set; }

        public JobBuilder WithJob<T>()
            where T : class, IJob
        {
            JobType = typeof(T);

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

        public JobBuilder WithData(JobData data)
        {
            JobData = data;

            return this;
        }

        public void Build()
        {
            if (JobType == null)
                throw new ArgumentNullException(nameof(JobType));

            if (JobData == null)
                throw new ArgumentNullException(nameof(JobData));

            if (typeof(IDateJob).IsAssignableFrom(JobType) && !JobStartDate.HasValue)
                throw new InvalidOperationException($"Can't created an {nameof(IDateJob)} without the {nameof(JobStartDate)} setted.");
        }
    }
}