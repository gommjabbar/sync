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
using Sinq.Converters;

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
    }
}