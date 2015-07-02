using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sinq.Models
{
    public class ActivityTime
    {
        public int Id { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }
    }
}