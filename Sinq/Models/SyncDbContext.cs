using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Sinq.Models
{
   /* public class SyncDbContext : IdentityDbContext<ApplicationUser>
    {
        public SyncDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Activity> Activities { get; set; } 

        public static SyncDbContext Create()
        {
            return new SyncDbContext();
        }
    }*/

    public class SyncDbContext : DbContext
    {
        public SyncDbContext()
            : base("SyncDbConnectionString")
        {

        }
        public static SyncDbContext Create()
        {
            return new SyncDbContext();
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityTime> ActivitiesTime { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

        //override SaveChanges() method

        public override int SaveChanges()
        {
            DateTimeOffset saveTime = DateTimeOffset.Now;

            foreach (var entry in this.ChangeTracker.Entries().Where(e => (e.State == EntityState.Added || e.State == EntityState.Modified)))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Property("CreateDate").CurrentValue = saveTime;
                        break;
                    case EntityState.Modified:
                        entry.Property("UpdateDate").CurrentValue = saveTime;
                        break;
                }
            }

            return base.SaveChanges();

        }

    }
}