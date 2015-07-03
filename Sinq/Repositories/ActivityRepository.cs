using Sinq.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Sinq.Repositories
{
    public class ActivityRepository: GenericRepository<Activity>, 
         IDisposable
    {
        
        public ActivityRepository(SyncDbContext context)
            : base(context)
        {
        
        }


        public void Dispose()
        {
           
        }
    }
}