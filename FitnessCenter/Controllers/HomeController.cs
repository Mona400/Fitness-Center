using FitnessCenter.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace FitnessCenter.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Course()
        {
            var courses = db.Courses;
            return View(courses.ToList());
        }
        public ActionResult Details(int CourseId)
        {
            var course = db.Courses.Find(CourseId);
            if (course == null)
            {
                return HttpNotFound();
            }
            Session["CourseId"] = CourseId;
            return View(course);
        }

        [Authorize]
        public ActionResult Apply()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Apply(string Message)
        {
            var UserId = User.Identity.GetUserId();
            var CourseId = (int)Session["CourseId"];

            var check = db.ApplyForCourses.Where(a => a.CourseId == CourseId && a.UserId == UserId).ToList();
            if (check.Count < 1)
            {
                var course = new ApplyForCourse();
                course.UserId = UserId;
                course.CourseId = CourseId;
                course.Message = Message;
                course.ApplyDate = DateTime.Now;
                db.ApplyForCourses.Add(course);
                db.SaveChanges();
                ViewBag.Result = "تم الحجز بنجاح.....!";
            }
            else
            {
                ViewBag.Result = " لقد تم الحجز مسبقا بالفعل .....!";
            }
            return View();


        }
        [Authorize]
        public ActionResult GetCourseByUser()
        {
            var UserId = User.Identity.GetUserId();
            var courses = db.ApplyForCourses.Where(a => a.UserId == UserId);
            return View(courses.ToList());
        }

        [Authorize]
        public ActionResult DetailsOfCourse(int id)
        {
            var course = db.ApplyForCourses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }

            return View(course);
        }
        [Authorize]
        public ActionResult GetCoursesByCoach()
        {
            var UserID = User.Identity.GetUserId();
            var Jobs = from app in db.ApplyForCourses
                       join Job in db.Courses
                       on app.CourseId equals Job.Id
                       where Job.User.Id == UserID
                       select app;
            var grouped = from j in Jobs
                          group j by j.Job.courseTitle
                        into gr
                          select new CoursesViewModel
                          {
                              CourseTitle = gr.Key,
                              Items = gr
                          };
            return View(grouped.ToList());

        }

        public ActionResult Edit(int id)
        {
            var course = db.ApplyForCourses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }


        [HttpPost]

        public ActionResult Edit(ApplyForCourse course)
        {

            if (ModelState.IsValid)
            {
                course.ApplyDate = DateTime.Now;
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetCourseByUser");
            }
            return View(course);

        }

        public ActionResult Delete(int id)
        {
            var course = db.ApplyForCourses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }


        [HttpPost]

        public ActionResult Delete(ApplyForCourse job)
        {

            // TODO: Add delete logic here

            var myCourse = db.ApplyForCourses.Find(job.Id);
            db.ApplyForCourses.Remove(myCourse);
            db.SaveChanges();
            return RedirectToAction("GetCourseByUser");


        }


        public ActionResult Search()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.Courses.Where(a => a.courseTitle.Contains(searchName)).ToList();
            //|| a.courseContent.Contains(searchName)
            //|| a.coachName.Contains(searchName)
            // || a.coachDescribtion.Contains(searchName)).ToList();



            return View(result);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("email@gmail.com");
                mail.To.Add("monasallam400@gmail.Com");
                mail.Subject = contact.Subject;
                mail.Body = "Sender Name: " + contact.Name + "<br>" +
                              "Email: " + contact.Email + "<br>" +
                              "Subject:" + contact.Subject + "<br>" +
                               "Message:<b>" + contact.Message + "</b>";
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("C:\\file.zip"));

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("monasallam400@gmail.com", "m01091600295m");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
                return RedirectToAction("Index");
            }

            //var mail = new MailMessage();
            //var LoginInfo = new NetworkCredential("monasallam400@gmail.Com", "m01091600295m");
            //mail.From = new MailAddress(contact.Email);
            //mail.To.Add(new MailAddress("monasallam400@gmail.Com"));
            //mail.Subject = contact.Subject;
            //mail.IsBodyHtml = true;
            //string body="Sender Name: "+contact.Name+"<br>"+
            //              "Email: "+contact.Email+"<br>"+
            //              "Subject:"+contact.Subject+"<br>"+
            //               "Message:<b>" + contact.Message+ "</b>";
            //mail.Body = body;

            //var smtpClient = new SmtpClient("smtp.gmail.com",587);
            //smtpClient.Credentials = LoginInfo;
            //smtpClient.Send(mail);
            //return RedirectToAction("Index");
        }

    }
}