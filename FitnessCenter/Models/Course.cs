using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FitnessCenter.Models
{
    [Table("Courses")]
    public class Course
    {
        public int Id { get; set; }

        [DisplayName("Course Name")]
        public string courseTitle { get; set; }
        [DisplayName("Course Discribtion")]
        [AllowHtml]
        public string courseContent { get; set; }
        [DisplayName("Course Photo")]
        public string courseImage { get; set; }
        [DisplayName("Coach Name")]
        public string coachName { get; set; }
        [DisplayName("Coach Discribtion")]
        public string coachDescribtion { get; set; }
        [DisplayName("Coach Photo")]
        public string coachImage { get; set; }
        public  string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }


    }
}