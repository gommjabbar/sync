using Sinq.Models;
using Sinq.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sinq.Controllers
{
    public class ActivitiesApiController : ApiController
    {
         private IActivityRepository activityRepository;

         public ActivitiesApiController()
         {
             this.activityRepository = new ActivityRepository(new SyncDbContext());
         }

        [Route("api/activities")]
        [HttpPost]
         public Activity Create(Activity activity)
        {
            if (ModelState.IsValid)
            {
                //in metoda update din repository
                //db.Entry(activity).State = EntityState.Modified;
                activityRepository.Add(activity);

                //db.SaveChanges();
                activityRepository.SaveChanges();
                return activity;
            }
            return null;
        }
        /*
        [Route("api/activities")]
        [HttpGet]
        public Activity Get()
        {
            if (ModelState.IsValid)
            {
                //in metoda update din repository
                //db.Entry(activity).State = EntityState.Modified;
                activityRepository.GetActivities();

                //db.SaveChanges();
                activityRepository.SaveChanges();
                return activity;
            }
            return null;
        }
        */


        // GET: Activities 
        [Route("api/activities")]
        [HttpGet]
        public IEnumerable < Activity> Get()
        {
            if (ModelState.IsValid)
            {
                //in metoda update din repository
                //db.Entry(activity).State = EntityState.Modified;
                
              //  activityRepository.GetActivities();
                
                //db.SaveChanges();
               // activityRepository.SaveChanges();
                return activityRepository.GetActivities();
                
            }
            return null;
        }


    }
}
