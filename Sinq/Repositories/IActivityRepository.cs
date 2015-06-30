using Sinq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sinq.Repositories
{
   public interface IActivityRepository:IDisposable
    {
        IEnumerable<Activity> GetActivities();
        Activity FindActivityBy(int id);
        void Add(Activity activity); 
        void Remove(int activityID);
        void Update(Activity activity);
        void SaveChanges();

        Activity FindActivityBy(int? id);
    }
}
