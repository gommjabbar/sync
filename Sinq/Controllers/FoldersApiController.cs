using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Sinq.Models;
using Sinq.Repositories;
using Sinq.Response;
using AutoMapper;
using Sinq.DTO;

namespace Sinq.Controllers
{
    [RoutePrefix("api/folders")]
    public class FoldersApiController : ApiController
    {
        private IFolderRepository _fd = new FolderRepository();
        private IActivityUnitOfWork _auow = new ActivityUnitOfWork()  ;

        
        /// <summary>
        // The method returns the list with all folders.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public JsonCollectionResponse<Folder> GetAll()
        {
         return new JsonCollectionResponse<Folder>(Request, () =>
            {
                var allFolders = _fd.GetAll();
                return allFolders.Select(Mapper.Map<Folder>).ToList();
            });
        }

        /// <summary>
        /// The method add a new folder.
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        public JsonResponse<Folder> CreateFolder(Folder folder)
        {
            return new JsonResponse<Folder>(Request, () =>
            {
                if (folder.Name != "Inbox")
                {
                    var Folder = Mapper.Map<Folder>(folder);
                    _fd.Insert(folder);
                    _fd.Save();
                    return Mapper.Map<Folder>(folder);
                }
                else
                {
                    throw new Exception("The Inbox folder already exists");
                }
            });
        }

        /// <summary>
        /// The method delete a specified folder.
        /// </summary>
        /// <param name="folderId"></param>
        /// <returns></returns>
        [Route("{folderId:int}")]
        [HttpDelete]
        public JsonResponse<bool> DeleteFolder(int folderId)
        {
            return new JsonResponse<bool>(Request, () =>
            {
                var folder = _fd.GetByID(folderId);
                if (folder.Name.Equals("Inbox"))
                {
                    throw new Exception("Nu puteti sterge folderul cu numele Inbox!!!");
                }
                else
                {
                    var activities = folder.Activities;
                    foreach (var act in activities)
                    {
                        var actTimes = act.ActivityTimes;
                        foreach (var actT in actTimes)
                        {
                            _fd.Delete(actT.Id);
                        }
                        _fd.Delete(act.Id);
                    }
                    var result = _fd.Delete(folder.Id);
                    _fd.Save();
                    if (result)
                    {
                        return true;
                    }
                }
                return false;
            });
        }


        //[Route("api/folders/{folderId}‏")]
        //[HttpGet]
        //public JsonCollectionResponse<ActivityDTO> GetActFromFolder(int id)
        //{
        //    return new JsonCollectionResponse<ActivityDTO>(Request, () =>
        //    {
        //        var folder = _fd.GetByID(id);
        //        if (folder != null)
        //        {
        //            var activity = folder.Activities;
        //            return activity.Select(Mapper.Map<ActivityDTO>).ToList();

        //        }
        //        return null;
        //    });
        //}


        //[Route("api/folders/test")]
        //[HttpGet]
        //public JsonCollectionResponse<ActivityDTO> GetActNotCompletedTest(int folderId)
        //{
        //    bool completed = false;
        //    return new JsonCollectionResponse<ActivityDTO>(Request, () =>
        //    {
        //        return null;
        //    });
        //}

        /// <summary>
        /// The method get the list of uncompleted activities.
        /// </summary>
        /// <param name="folderId"></param>
        /// <param name="completed"></param>
        /// <returns></returns>
        [Route("{folderId:int}/activities")]
        [HttpGet]
        public JsonCollectionResponse<ActivityDTO> GetFolderActivities(int folderId, [FromBody]bool completed)
        {
           // bool completed = false;
            return new JsonCollectionResponse<ActivityDTO>(Request, () =>
            {
                var folder = _fd.GetByID(folderId);
                
                if (folder != null)
                {
                    var factivities = folder.Activities.Count;
                    var activities = folder.Activities.Select(a => a.Completed == completed);     
                    return activities.Select(a => Mapper.DynamicMap<ActivityDTO>(a)).ToList();
                    }
                return null;
            });
        }

       
        /// <summary>
        /// The method add a new activity in the specified folder.
        /// </summary>
        /// <param name="folderId"></param>
        /// <param name="activity"></param>
        /// <returns></returns>
        [Route("{folderId:int}/activities")]
        [HttpPost]
        public JsonResponse<ActivityDTO> CreateActivity(int folderId, ActivityDTO activity)
        {
            return new JsonResponse<ActivityDTO>(Request, () =>
            {
               var folder = _fd.GetByID(folderId);
                if (folder != null)
                {
                    var activityEO = Mapper.Map<Activity>(activity);
                    activityEO.FolderId = folderId;
                    _auow.ActivityRepository.Insert(activityEO);
                    _auow.Save();
                    return Mapper.Map<ActivityDTO>(activityEO);
                }
                return null;
            });
        }


        
       
        //public JsonResponse <Folder> FindFolderById(int? id)
        //{
        //    return new JsonResponse<Folder>(Request, () =>
        //    {
        //        Folder folder = new Folder();
        //        folder = _fd.GetByID(id);
        //        return Mapper.Map<Folder>(folder);
        //    });
        //}

        //public JsonResponse<Folder> FindFolderByName(string Name)
        //{
        //    return new JsonResponse<Folder>(Request, () =>
        //    {
        //        Folder folder = new Folder();
        //        if (folder.Name.Equals(Name)!=null) { 
        //            var fold = _fd.GetByID(folder.Id);
        //            return Mapper.Map<Folder>(fold);
        //        }
        //        return Mapper.Map<Folder>(folder);
        //    });
        //}

    }
}