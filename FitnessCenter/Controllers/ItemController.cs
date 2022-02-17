using FitnessCenter.Models;
using FitnessCenter.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FitnessCenter.Controllers
{
  
    public class ItemController : Controller
    {
        // GET: Item
        public ApplicationDbContext db = new ApplicationDbContext();
        private List<OrderDetailsModel> ListOfShoppingCartModels=new List<OrderDetailsModel>();
       public ActionResult Index()
        {
            return View(db.Items.ToList());
            
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemViewModel item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Item/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Item/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemViewModel item, HttpPostedFileBase uploadImage)
        {
           
            if (ModelState.IsValid )
            {
               
                string pathItem = Path.Combine(Server.MapPath("~/Uploads"), uploadImage.FileName);
               
                uploadImage.SaveAs(pathItem);
              
                item.ImagePath = uploadImage.FileName;


                item.UserID = User.Identity.GetUserId();
                db.Items.Add(item);

                db.SaveChanges();
                return RedirectToAction("Index");

              
            }
            //ViewBag.categoryId = new SelectList(db.Courses, "Id", "categoryName", course.Id);
            return View(item);
        }


        // GET: Item/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemViewModel item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ItemViewModel item, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                string oldpathImage = Path.Combine(Server.MapPath("~/Uploads"), item.ImagePath);
                if (uploadImage != null)
                {
                    System.IO.File.Delete(oldpathImage);
                    string pathCourse = Path.Combine(Server.MapPath("~/Uploads"), uploadImage.FileName);
                    uploadImage.SaveAs(pathCourse);
                    item.ImagePath = uploadImage.FileName;

                }
            
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Item/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           var item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
      
        public ActionResult DeleteFromCart(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = db.OrderDetails.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("DeleteFromCart")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedFromCart(int id)
        {
            var item = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       

    }
}
