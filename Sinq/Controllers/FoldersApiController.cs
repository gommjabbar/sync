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



        [Route("api/folders")]
        [HttpPost]
        public JsonResponse<Folder> Create(Folder folder)
        {
            return new JsonResponse<Folder>(Request, () =>
            {
                var Folder = Mapper.Map<Folder>(folder);
                _fd.Insert(folder);
                _fd.Save();
                return Mapper.Map<Folder>(folder);
                });
        }
               

        //Nu stiu daca e ok delete-ul !!!
        [Route("api/folders/{id}")]
        [HttpDelete]
        public JsonResponse<bool> Delete(int id)
        {
            return new JsonResponse<bool>(Request, () =>
            {
                Folder folder = new Folder();
                if (folder.Name != "Inbox")
                {
                    Activity activity = new Activity();
                    ActivityTime act = new ActivityTime();

                    //var res = _fd.Get(activity).Delete(id);

                    var re = _fd.Delete(act.ActivityId);
                    var res = _fd.Delete(activity.Id);
                    
                    if ((re==true)&&(res==true)) 
                    {
                        var result = _fd.Delete(id);
                        if (result)
                            _fd.Save();
                        return result;
                    }
                }
                else {
                    throw new Exception("Nu puteti sterge folderul cu numele Inbox!!!");
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

    }
}