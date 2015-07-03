using Sinq.Models;
using Sinq.Repositories;
using Sinq.Response;
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


        /// <summary>
        /// This method will add a new activity in the database.
        /// </summary>
        /// <param name="activity"></param>
        /// <returns>activity</returns>
        [Route("api/activities")]
        [HttpPost]
         public JsonResponse<Activity> Create(Activity activity)
        {
            return new JsonResponse<Activity>(Request, () =>
            {
                activityRepository.Add(activity);
                activityRepository.SaveChanges();
                return activity;
            });
        }
        

        /// <summary>
        /// This method will return all activities.
        /// </summary>
        /// <returns>The list off all activities from the database.</returns>
        [Route("api/activities")]
        [HttpGet]
        public JsonCollectionResponse<Activity> Get()
        {
            return new JsonCollectionResponse<Activity>(Request, () => {
                var activities = activityRepository.GetActivities();
                return activities.ToList();            
            });
        }

      
        /// <summary>
        /// This method will delete the given activity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true - if the activity was deleted; false - if the activity could not be deleted</returns>
        [Route("api/activities/{id}")]
        [HttpDelete]
        public JsonResponse<bool> Delete(int id)
        {
            return new JsonResponse<bool>(Request, () =>
           {
               Activity activity = new Activity();
               activity = activityRepository.FindActivityBy(id);
               if (activity != null)
               {
                   activityRepository.Remove(id);
                   activityRepository.SaveChanges();
                   return true;
               }
      
               else { return false; }   
           });

        }

      
        /// <summary>
        /// This method will update the 'Completed' proprety of a given activity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true - if the activity was updated; false - if the activity could not be deleted</returns>
        [Route("api/activities/{id}/complete")]
        [HttpDelete]
        public JsonResponse<bool> Put(int id)
        {
            return new JsonResponse<bool>(Request, () =>
            {
                Activity activity= new Activity();
                activity = activityRepository.FindActivityBy(id);
                if (activity != null)
                {
                    activityRepository.Update(activity);
                    activityRepository.SaveChanges();
                    return true;
                }
                else { return false; }
                

            });
        }

        /// <summary>
        /// This method will search an activity specified by Id in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>activity</returns>
        // GET: Activities/Details/5
        public JsonResponse<Activity> FindActivityById(int? id)
        {
            return new JsonResponse<Activity>(Request, () =>
           {
               Activity activity = new Activity();
               activity = activityRepository.FindActivityBy(id);
               return activity;
           });
        }


    }
}
