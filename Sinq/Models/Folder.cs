using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sinq.Models
{
    public class Folder : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
     
        //public Folder() {
        //    this.Name = "Inbox";
        //}
    }
}