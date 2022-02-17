using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessCenter.Models
{
    public class CoursesViewModel
    {
      
        public string CourseTitle { get; set; }
        public IEnumerable<ApplyForCourse> Items { get; set; }
    }
}