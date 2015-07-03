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
      
        private IActivityRepository activityRepository;

        public ActivitiesController() {

             this.activityRepository = new ActivityRepository(new SyncDbContext());
       }

        public ActivitiesController(IActivityRepository activityRepository){
         
            this.activityRepository = activityRepository;
      }


      



        // GET: Activities
        public ActionResult Index()
        {
       
            return View(activityRepository.GetActivities());
        }

        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
         
            Activity activity = activityRepository.FindActivityBy(id);
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
               
                activityRepository.Add(activity);
              
                activityRepository.SaveChanges();
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
           
            Activity activity = activityRepository.FindActivityBy(id);
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
                
                activityRepository.Update(activity);

                activityRepository.SaveChanges();
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
          
            Activity activity = activityRepository.FindActivityBy(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
          
            activityRepository.Remove(id);
         
            activityRepository.SaveChanges();
            return RedirectToAction("Index");
        }

    
    }
}
