using Sinq.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Sinq.Repositories
{
    public class ActivityRepository: IActivityRepository , IDisposable
    {
        private SyncDbContext db;


        public ActivityRepository(SyncDbContext db) {
            this.db = db;
        }

        //get all activities from the database
        public IEnumerable<Activity> GetActivities()
        {
            return db.Activities.ToList();
        }

        //get a specified activity(by id)
        public Activity FindActivityBy(int? id)
        {
            return db.Activities.Find(id);
        }

        //add an activity
        public void Add(Activity activity)
        {
            db.Activities.Add(activity);
        }

        //delete an activity
        public void Remove(int activityID)
        {
            Activity activity = db.Activities.Find(activityID);
            db.Activities.Remove(activity);
        }

        //update an activity
        public void Update(Activity activity)
        {
            db.Entry(activity).State = EntityState.Modified;
        }

        //save the changes
        public void SaveChanges()
        {
            db.SaveChanges();
        }

       

        public void Dispose()
        {
            throw new NotImplementedException();
        }


        public Activity FindActivityBy(int id)
        {
            throw new NotImplementedException();
        }
    }
}