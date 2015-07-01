using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sinq.Models
{
    public class Activity : BaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTimeOffset? DueDate { get; set; }
    }
}