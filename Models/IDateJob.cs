using System;
using System.Threading.Tasks;

namespace QuartzNETDependencyInversionSample.Models
{
    public interface IDateJob : IJob
    {
        Task<DateTime> GetNextDate(JobData data);
    }
}