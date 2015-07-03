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
         private IActivityUnitOfWork _activityUnitOfWork;

         public ActivitiesApiController()
         {
             this._activityUnitOfWork = new ActivityUnitOfWork();
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
                _activityUnitOfWork.ActivityRepository.Insert(activity);
                _activityUnitOfWork.Save();
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
                var activities = _activityUnitOfWork.ActivityRepository.Get();
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
               activity = _activityUnitOfWork.ActivityRepository.GetByID(id);
               if (activity != null)
               {
                   _activityUnitOfWork.ActivityRepository.Delete(id);
                   _activityUnitOfWork.Save();
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
                activity = _activityUnitOfWork.ActivityRepository.GetByID(id);
                if (activity != null)
                {
                    _activityUnitOfWork.ActivityRepository.Update(activity);
                    _activityUnitOfWork.Save();
                    return true;
                }
                else { return false; }
                

            });
        }
        [Route("api/activities/{id}/start")]
        [HttpPost]
        public JsonResponse<bool> StartActivity(int id)
        {
            return new JsonResponse<bool>(Request, () =>
            {
                var activityTime = _activityUnitOfWork.StartActivity(id);
                return true;
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
               activity = _activityUnitOfWork.ActivityRepository.GetByID(id);
               return activity;
           });
        }


    }
}
