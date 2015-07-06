using Sinq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sinq.DTO
{
    public class ActivityDTO
    {
        public int Id { get; set; }
        public bool Completed { get; set; }
        public string Name { get; set; }
        public int ElapsedTime { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public bool IsStarted { get; set; }

    }
}