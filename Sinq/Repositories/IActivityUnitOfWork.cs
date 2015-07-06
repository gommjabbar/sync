using Sinq.Models;
using System;
namespace Sinq.Repositories
{
    public interface IActivityUnitOfWork
    {
        IGenericRepository<Sinq.Models.Activity> ActivityRepository { get; }
        IGenericRepository<Sinq.Models.ActivityTime> ActivityTimeRepository { get; }
        
        void Save();

        ActivityTime StartActivity(int id);
        ActivityTime StopActivity(int id);

    
    }
}
