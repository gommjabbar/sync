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
        /// <returns></returns>
        [Route("api/activities")]
        [HttpPost]
         public JsonResponse<Activity> Create(Activity activity)
        {
            /*if (ModelState.IsValid)
            {
              
                activityRepository.Add(activity);

               
                activityRepository.SaveChanges();
                return activity;
            }
            return null;*/

            return new JsonResponse<Activity>(Request, () =>
            {
              // Activity activity = Mapper.Map<Activity>(activity);
                activityRepository.Add(activity);
                activityRepository.SaveChanges();

             // return Mapper.Map<Activity>(activity);
                return activity;
            });
        }
        

        /// <summary>
        /// This method will return all activities.
        /// </summary>
        /// <returns></returns>
        [Route("api/activities")]
        [HttpGet]
        public JsonCollectionResponse<Activity> Get()
        {
          /*  if (ModelState.IsValid)
            {
                return activityRepository.GetActivities();
            }
            return null;*/

            return new JsonCollectionResponse<Activity>(Request, () => {
                var activities = activityRepository.GetActivities();
               // return activities.Select(Mapper.Map<Activity>).ToList();
                return activities.ToList();            
            });
        }


        //trebuie verificat daca e ok
        /// <summary>
        /// This method will delete the given activity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/activities/{id}")]
        [HttpDelete]
        public JsonResponse<bool> Delete(int id)
        {
           /* if (ModelState.IsValid)
            {
                activityRepository.Remove(id);  
                activityRepository.SaveChanges();
            }
            return null;*/
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


        //trebuie verificat daca e ok asa
        /// <summary>
        /// This method will update the 'complete' proprety of a given activity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/activities/{id}/complete")]
        [HttpDelete]
        public JsonResponse<bool> Put(int id)
        {
            /*if (ModelState.IsValid)
            {
                if (activityRepository == null)
                {
                 //???   return false;
                }
                activityRepository.SaveChanges();
               // ???return Ok<bool>(true);
            }
            return null;*/

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

    }
}
