using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessCenter.Models
{
    public class ApplyForCourse
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime ApplyDate { get; set; }
        public  int CourseId { get; set; }
        public  string UserId { get; set; }
        public virtual Course Job { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}