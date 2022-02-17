using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FitnessCenter.Models;
using Microsoft.AspNet.Identity;

namespace FitnessCenter.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Courses
        public ActionResult Index()
        {
            return View(db.Courses.ToList());
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course course, HttpPostedFileBase uploadCourse, HttpPostedFileBase uploadCoach)
        {
            if (ModelState.IsValid)
            {

                string pathCourse = Path.Combine(Server.MapPath("~/Uploads"), uploadCourse.FileName);
                string pathCoach = Path.Combine(Server.MapPath("~/Uploads"), uploadCoach.FileName);
                uploadCourse.SaveAs(pathCourse);
                uploadCoach.SaveAs(pathCoach);

                course.courseImage = uploadCourse.FileName;
                course.coachImage = uploadCoach.FileName;

                course.UserID = User.Identity.GetUserId();
                db.Courses.Add(course);

                db.SaveChanges();
                return RedirectToAction("Course", "Home"); 
            }
            //ViewBag.categoryId = new SelectList(db.Courses, "Id", "categoryName", course.Id);
            return View(course);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Course course, HttpPostedFileBase uploadCourse, HttpPostedFileBase uploadCoach)
        {
            if (ModelState.IsValid)
            {
                string oldpathCourse = Path.Combine(Server.MapPath("~/Uploads"), course.courseImage);
                if (uploadCourse != null)
                {
                    System.IO.File.Delete(oldpathCourse);
                    string pathCourse = Path.Combine(Server.MapPath("~/Uploads"), uploadCourse.FileName);
                    uploadCourse.SaveAs(pathCourse);
                    course.courseImage = uploadCourse.FileName;

                }
                string oldpathCoach = Path.Combine(Server.MapPath("~/Uploads"), course.coachImage);
                if (uploadCoach != null)
                {
                    System.IO.File.Delete(oldpathCoach);
                    string pathCoach = Path.Combine(Server.MapPath("~/Uploads"), uploadCoach.FileName);
                    uploadCoach.SaveAs(pathCoach);
                    course.coachImage = uploadCoach.FileName;

                }

                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
