using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sinq.Models;
using Sinq.Repositories;

namespace Sinq.Controllers
{
    public class ActivitiesController : Controller
    {
        private IActivityUnitOfWork _activityUnitOfWork;

        public ActivitiesController() {

             this._activityUnitOfWork = new ActivityUnitOfWork();
        }

        public ActivitiesController(IActivityUnitOfWork activityUnitOfWork)
        {
            this._activityUnitOfWork = activityUnitOfWork;
        }


        // GET: Activities
        public ActionResult Index()
        {
       
            return View(_activityUnitOfWork.ActivityRepository.Get());
        }

        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
         
            Activity activity = _activityUnitOfWork.ActivityRepository.GetByID(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Activities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Name,DueDate")] Activity activity)
        {
            if (ModelState.IsValid)
            {
               
                _activityUnitOfWork.ActivityRepository.Insert(activity);
              
                _activityUnitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(activity);
        }

        // GET: Activities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            Activity activity = _activityUnitOfWork.ActivityRepository.GetByID(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CreateDate,DueDate")] Activity activity)
        {
            if (ModelState.IsValid)
            {

                _activityUnitOfWork.ActivityRepository.Update(activity);

                _activityUnitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(activity);
        }

        // GET: Activities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Activity activity = _activityUnitOfWork.ActivityRepository.GetByID(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        
    
    }
}
