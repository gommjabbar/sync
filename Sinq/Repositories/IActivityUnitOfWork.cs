using Sinq.Models;
using System;
namespace Sinq.Repositories
{
    public interface IActivityUnitOfWork
    {
        GenericRepository<Sinq.Models.Activity> ActivityRepository { get; }
        GenericRepository<Sinq.Models.ActivityTime> ActivityTimeRepository { get; }
        void Save();

        ActivityTime StartActivity(int id);
        ActivityTime EndActivity(int id);
    }
}
