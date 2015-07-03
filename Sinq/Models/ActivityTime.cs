using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sinq.Models
{
    public class ActivityTime : BaseEntity
    {
        public int Id { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }
    }
}