using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sinq.Models
{
    public class Activity : BaseEntity
    {
        public Activity()
        {
            ActivityTimes = new List<ActivityTime>();
        }
        public int Id { get; set; }
        public bool Completed { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string Name { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public virtual ICollection<ActivityTime> ActivityTimes { get; set; }

        //[ForeignKey("Folder")]
        //public int FolderId { get; set; }
        //public virtual Folder Folder { get; set; }

        public bool IsStarted()
        {
            return this.ActivityTimes.Any(a => a.EndDate != null);
        }
    }
}