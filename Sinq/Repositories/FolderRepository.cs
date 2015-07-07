using Sinq.Models;
using Sinq.Models.Folder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sinq.Repositories
{
    public class FolderRepository : GenericRepository<Folder>, IFolderRepository
    {
         public FolderRepository(SyncDbContext context)
            : base(context)
        {
        
        }

         public FolderRepository()
         {
             // TODO: Complete member initialization
         }
         public void Save() { 
            context.SaveChanges(); 
         }

    }
}