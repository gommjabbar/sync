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

        public ActivityRepository()
        {
            db = new SyncDbContext();
        }
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
            //var result = db.Activities
            //    .Where(activity => activity.Id == activityID)
            //    .FirstOrDefault();
            var result = db.Activities.FirstOrDefault(activity => activity.Id == activityID);

            db.Activities.Remove(result);
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
            var result = db.Activities.FirstOrDefault(activity => activity.Id == id);
            return result;
        }

        public ActivityTime StartActivity(int id)
        {
            return null; 
        }
        public ActivityTime EndActivity(int id)
        {
            return null;
        }
    }
}