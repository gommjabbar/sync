using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sinq.Models
{
    public class BaseEntity
    {
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset? UpdateDate { get; set; }
    }
}