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
using Sinq.Converters;


namespace Sinq.Controllers
{
   // [RoutePrefix("api/activities")]
    [RoutePrefix("api/folders/{folderId:int}/activities")]
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

        /// <summary>
        /// The method add a new activity in the specified folder.
        /// </summary>
        /// <param name="folderId"></param>
        /// <param name="activity"></param>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        public JsonResponse<ActivityDTO> CreateActivity(int folderId, ActivityDTO activity)
        {
            return new JsonResponse<ActivityDTO>(Request, () =>
            {
                var folder = _activityUnitOfWork.ActivityRepository.Get(a => a.FolderId == folderId);
                if (folder != null)
                {
                    var activityEO = Mapper.Map<Activity>(activity);
                    activityEO.FolderId = folderId;
                    _activityUnitOfWork.ActivityRepository.Insert(activityEO);
                    _activityUnitOfWork.Save();
                    return Mapper.Map<ActivityDTO>(activityEO);
                }
                return null;
            });
        }


        /// <summary>
        /// This method will update the 'Completed' proprety of a given activity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true - if the activity was updated; false - if the activity could not be deleted</returns>
        [Route("{activityId:int}/complete")]
        [HttpDelete]
        public JsonResponse<bool> UpdateCompleted(int folderId, int activityId, bool complete)
        {
            return new JsonResponse<bool>(Request, () =>
            {
                
                var folder = _activityUnitOfWork.ActivityRepository.Get(a => a.FolderId == folderId);
                if (folder != null)
                {
                    Activity activity = new Activity();
                    activity = _activityUnitOfWork.ActivityRepository.GetByID(activityId);
                    if (activity != null)
                    {
                        _activityUnitOfWork.ActivityRepository.Update(activity);
                        _activityUnitOfWork.Save();
                        return true;
                    }
                    else { return false; }
                }
                return false;
            });
           
        }

        /// <summary>
        /// The method get the list of completed/uncompleted activities.
        /// </summary>
        /// <param name="folderId"></param>
        /// <param name="completed"></param>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public JsonCollectionResponse<ActivityDTO> GetFolderActivities(int folderId, [FromBody]bool completed)
        {
            // bool completed = false;
            return new JsonCollectionResponse<ActivityDTO>(Request, () =>
            {
                List<Activity> resultedActivities=new List<Activity>();

                var activities = _activityUnitOfWork.ActivityRepository.Get(a => a.FolderId == folderId);
                foreach (Activity activity in activities) {
                    if (activity.Completed == completed) {
                         resultedActivities.Add(activity);
                    }
                }
                var result = resultedActivities.Select(
                        a => GenericConverter.Map<Activity, ActivityDTO>(a)).ToList();
                    return result;
               
            });
        }

        
        ///// <summary>
        ///// This method will add a new activity in the database.
        ///// </summary>
        ///// <param name="activity"></param>
        ///// <returns>activity</returns>
        //[Route("")]
        //[HttpPost]
        //public JsonResponse<ActivityDTO> Create(ActivityDTO activity)
        //{
        //    return new JsonResponse<ActivityDTO>(Request, () =>
        //    {
        //        var activityDTO = Mapper.Map<Activity>(activity);
        //        _activityUnitOfWork.ActivityRepository.Insert(activityDTO);
        //        _activityUnitOfWork.Save();
        //        return Mapper.Map<ActivityDTO>(activity);
        //    });
        //}


        /// <summary>
        /// This method will delete the given activity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true - if the activity was deleted; false - if the activity could not be deleted</returns>
        //[Route("{id:int}")]
        //[HttpDelete]
        //public JsonResponse<bool> Delete(int id)
        //{
        //    return new JsonResponse<bool>(Request, () =>
        //   {
        //       Activity activity = new Activity();

        //       var result = _activityUnitOfWork.ActivityRepository.Delete(id);
        //       if (result)
        //           _activityUnitOfWork.Save();
        //       return result;
        //   });

        //}


        /// <summary>
        /// This method will update the 'Completed' proprety of a given activity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true - if the activity was updated; false - if the activity could not be deleted</returns>
        [Route("{activityId:int}/complete")]
        [HttpDelete]
        public JsonResponse<bool> Put(int folderId, int activityId)
        {
            return new JsonResponse<bool>(Request, () =>
            {
                Activity activity = new Activity();
                activity = _activityUnitOfWork.ActivityRepository.GetByID(activityId);
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
        [Route("{id:int}/start")]
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
        [Route("{id:int}/stop")]
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
        //public JsonResponse<ActivityDTO> FindActivityById(int? id)
        //{
        //    return new JsonResponse<ActivityDTO>(Request, () =>
        //   {
        //       Activity activity = new Activity();
        //       activity = _activityUnitOfWork.ActivityRepository.GetByID(id);
        //       return Mapper.Map<ActivityDTO>(activity);
        //   });
        //}


    }
}
