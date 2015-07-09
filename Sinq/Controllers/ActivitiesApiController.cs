using Sinq.DTO;
using Sinq.Models;
using Sinq.Repositories;
using Sinq.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;


namespace Sinq.Controllers
{
    public class ActivitiesApiController : ApiController
    {

        private IActivityUnitOfWork _activityUnitOfWork;

        public ActivitiesApiController()
        {
            this._activityUnitOfWork = new ActivityUnitOfWork();
        }

        public ActivitiesApiController(IActivityUnitOfWork activityUnitOfWork)
        {
            _activityUnitOfWork = activityUnitOfWork;
        }

        [Route("api/test‏")]
        [HttpGet]
        public JsonCollectionResponse<ActivityDTO> Test()
        {
            bool completed = false;
            return new JsonCollectionResponse<ActivityDTO>(Request, () =>
            {
                bool yes = false;
                //var folder = _activityUnitOfWork .GetByID(folderId);
                //if (folder != null)
                //{
                //    var activities = folder.Activities;
                //    foreach (var act in activities)
                //    {
                //        if (!act.Completed)
                //        {
                //            yes = true;
                //        }
                //    }
                //    if (yes)
                //    {
                //        return activities.Select(Mapper.Map<ActivityDTO>).ToList();
                //    }
                //}
                return null;
            });
        }


        /// <summary>
        /// This method will add a new activity in the database.
        /// </summary>
        /// <param name="activity"></param>
        /// <returns>activity</returns>
        [Route("api/activities")]
        [HttpPost]
        public JsonResponse<ActivityDTO> Create(ActivityDTO activity)
        {
            return new JsonResponse<ActivityDTO>(Request, () =>
            {
                var activityDTO = Mapper.Map<Activity>(activity);
                _activityUnitOfWork.ActivityRepository.Insert(activityDTO);
                _activityUnitOfWork.Save();
                return Mapper.Map<ActivityDTO>(activity);
            });
        }


        /// <summary>
        /// This method will return all activities.
        /// </summary>
        /// <returns>The list off all activities from the database.</returns>
        [Route("api/activities")]
        [HttpGet]
        public JsonCollectionResponse<ActivityDTO> GetAllActivities()
        {
            return new JsonCollectionResponse<ActivityDTO>(Request, () =>
            {
                var activitiesDTO = _activityUnitOfWork.ActivityRepository.GetAll();

                return activitiesDTO.Select(Mapper.Map<ActivityDTO>).ToList();
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

               var result = _activityUnitOfWork.ActivityRepository.Delete(id);
               if (result)
                   _activityUnitOfWork.Save();
               return result;
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
                Activity activity = new Activity();
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

        /// <summary>
        /// This method will set the state of the activity as 'started'
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if the satate of the activity is set to 'StartTime'</returns>
        [Route("api/activities/{id}/start")]
        [HttpPost]
        public JsonResponse<DateTimeOffset> StartActivity(int id)
        {
            return new JsonResponse<DateTimeOffset>(Request, () =>
            {
                var activityTime = _activityUnitOfWork.StartActivity(id);
                return activityTime.StartDate;
            });
        }


        /// <summary>
        /// This method will set the state of the activity as 'stoped'
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if the state of the activity is set to 'StopTime'</returns>
        [Route("api/activities/{id}/stop")]
        [HttpPost]
        public JsonResponse<bool> StopActivity(int id)
        {
            return new JsonResponse<bool>(Request, () =>
            {
                var activityTime = _activityUnitOfWork.StopActivity(id);
                return true;
            });
        }

        /// <summary>
        /// This method will search an activity specified by Id in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>activity</returns>
        // GET: Activities/Details/5
        public JsonResponse<ActivityDTO> FindActivityById(int? id)
        {
            return new JsonResponse<ActivityDTO>(Request, () =>
           {
               Activity activity = new Activity();
               activity = _activityUnitOfWork.ActivityRepository.GetByID(id);
               return Mapper.Map<ActivityDTO>(activity);
           });
        }


    }
}
