using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mvc_RealeState.Models;

namespace Mvc_RealeState.Controllers
{
    public class UserImageController : Controller
    {
        private MyCon db = new MyCon();

        // GET: UserImage
        public ActionResult Index()
        {
            var userImages = db.UserImages.Include(u => u.User);
            return View(userImages.ToList());
        }

        // GET: UserImage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserImage userImage = db.UserImages.Find(id);
            if (userImage == null)
            {
                return HttpNotFound();
            }
            return View(userImage);
        }

        // GET: UserImage/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: UserImage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( UserImage userImage,HttpPostedFileBase Name )
        {
            userImage.Name = System.IO.Path.GetFileName(Name.FileName);
         
            if (ModelState.IsValid)
            {
                db.UserImages.Add(userImage);
                db.SaveChanges();
                Name.SaveAs(Server.MapPath("../Upload/usersImage/" + userImage.Id.ToString() + "_" + userImage.Name));
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", userImage.UserId);
            return View(userImage);
        }

        // GET: UserImage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserImage userImage = db.UserImages.Find(id);
            if (userImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", userImage.UserId);
            return View(userImage);
        }

        // POST: UserImage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,UserId")] UserImage userImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", userImage.UserId);
            return View(userImage);
        }

        // GET: UserImage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserImage userImage = db.UserImages.Find(id);
            if (userImage == null)
            {
                return HttpNotFound();
            }
            return View(userImage);
        }

        // POST: UserImage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserImage userImage = db.UserImages.Find(id);
            db.UserImages.Remove(userImage);
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
