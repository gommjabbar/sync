﻿using Sinq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sinq.Repositories
{
    public class ActivityUnitOfWork : Sinq.Repositories.IActivityUnitOfWork
    {
        private SyncDbContext context = new SyncDbContext();
        private GenericRepository<Activity> _activityRepository;
        private GenericRepository<ActivityTime> _activityTimeRepository;


        public GenericRepository<Activity> ActivityRepository
        {
            get
            {

                if (this._activityRepository == null)
                {
                    this._activityRepository = new GenericRepository<Activity>(context);
                }
                return _activityRepository;
            }
        }
        public GenericRepository<ActivityTime> ActivityTimeRepository
        {
            get
            {

                if (this._activityTimeRepository == null)
                {
                    this._activityTimeRepository = new GenericRepository<ActivityTime>(context);
                }
                return _activityTimeRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public ActivityTime StartActivity(int id)
        {
            var activity = _activityRepository.GetByID(id);
            if (activity == null)
                throw new Exception("Activity not found");

            if (activity.ActivityTimes.Any(a => a.EndDate != null))
                throw new Exception("Activity is already started");

            var newActivityTime = new ActivityTime()// _activityTimeRepository.dbSet.Create();
            {
                StartDate = DateTimeOffset.Now,
                EndDate = null,
                ActivityId = activity.Id
            };
            activity.ActivityTimes.Add(newActivityTime);
            Save();

            return newActivityTime;
        }

    }
}