using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sinq.Models
{
    public class Folder : BaseEntity
    {

        public Folder()
        {
            Activities = new List<Activity>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        ICollection<Activity> Activities { get; set; }
    }
}
