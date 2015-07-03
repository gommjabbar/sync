using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sinq.Models
{
    public class BaseEnt
    {
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset? StopTime { get; set; }
    }
}
