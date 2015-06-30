using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sinq.Models
{
    public class SyncDbContext : IdentityDbContext<ApplicationUser>
    {
        public SyncDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static SyncDbContext Create()
        {
            return new SyncDbContext();
        }
    }
}