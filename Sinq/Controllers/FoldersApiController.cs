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

namespace Sinq.Controllers
{
    public class FoldersApiController : ApiController
    {
        private IFolderRepository _fd = new FolderRepository();

        /// <summary>
        /// This method will return all projects. If there are no projects an empty page will be returned.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/folders")]
        public JsonCollectionResponse<Folder> GetAll()
        {
            return new JsonCollectionResponse<Folder>(Request, () =>
            {
                var allFolders = _fd.GetAll();
                return allFolders.Select(Mapper.Map<Folder>).ToList();
            });
        }



        //[Route("api/folders")]
        //[HttpPost]
        //public JsonResponse<Folder> Create(Folder folder)
        //{
        //    return new JsonResponse<Folder>(Request, () =>
        //    {
        //        if (FindFolderByName("Inbox") == null)
        //        {
        //            var Folder = Mapper.Map<Folder>(folder);
        //            _fd.Insert(folder);
        //            _fd.Save();
        //            return Mapper.Map<Folder>(folder);
               
        //        }
        //        else {
        //            var Folder = Mapper.Map<Folder>(folder);
        //            _fd.Insert(folder);
        //            _fd.Save();
        //        }
        //        });
        //}

        [Route("api/folders")]
        [HttpPost]
        public JsonResponse<Folder> Create(Folder folder)
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
                else {
                    throw new Exception("Incerci sa adaugi una cu Inbox deja");
                }
            });
        }



        //Nu stiu daca e ok delete-ul !!!
        [Route("api/folders/{id}")]
        [HttpDelete]
        public JsonResponse<bool> Delete(int id)
        {
            return new JsonResponse<bool>(Request, () =>
            {
               // Folder folder = new Folder();
                var folder = _fd.GetByID(id);
                if (folder.Name.Equals("Inbox")) 
                {
                    throw new Exception("Nu puteti sterge folderul cu numele Inbox!!!");
                }
                else{
                    var activities = folder.Activities;
                    foreach (var act in activities) {
                        var actTimes = act.ActivityTimes;
                        foreach (var actT in actTimes) {
                            _fd.Delete(actT.Id);
                        }
                        _fd.Delete(act.Id);
                    }
                    var  result = _fd.Delete(folder.Id);
                    if (result) {
                        return true;
                    }
                }
                return false;
            });
        }

       
        public JsonResponse <Folder> FindFolderById(int? id)
        {
            return new JsonResponse<Folder>(Request, () =>
            {
                Folder folder = new Folder();
                folder = _fd.GetByID(id);
                return Mapper.Map<Folder>(folder);
            });
        }

        public JsonResponse<Folder> FindFolderByName(string Name)
        {
            return new JsonResponse<Folder>(Request, () =>
            {
                Folder folder = new Folder();
                if (folder.Name.Equals(Name)!=null) { 
                    var fold = _fd.GetByID(folder.Id);
                    return Mapper.Map<Folder>(fold);
                }
                return Mapper.Map<Folder>(folder);
            });
        }

    }
}