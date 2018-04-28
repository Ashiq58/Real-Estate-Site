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
    public class RegisterController : Controller
    {
        private MyCon db = new MyCon();

        // GET: Register
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.City).Include(u => u.UserType);
            return View(users.ToList());
        }

        // GET: Register/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Register/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name");
            //ViewBag.UserTypeId = new SelectList(db.UserTypes, "Id", "Name");
            return View();
        }

        // POST: Register/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,Contact,Email,Password,NId_orPassport,CityId,UserTypeId")] User user)
        {
            if (ModelState.IsValid)
            {
                user.UserTypeId = 2;
                db.Users.Add(user);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", user.CityId);
            ViewBag.UserTypeId = new SelectList(db.UserTypes, "Id", "Name", user.UserTypeId);
            return View(user);
        }

        // GET: Register/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", user.CityId);
            ViewBag.UserTypeId = new SelectList(db.UserTypes, "Id", "Name", user.UserTypeId);
            return View(user);
        }

        // POST: Register/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,Contact,Email,Password,NId_orPassport,CityId,UserTypeId")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", user.CityId);
            ViewBag.UserTypeId = new SelectList(db.UserTypes, "Id", "Name", user.UserTypeId);
            return View(user);
        }

        // GET: Register/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Register/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
