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
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

    }
}