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
    public class PropertyImageController : Controller
    {
        private MyCon db = new MyCon();

        // GET: PropertyImage
        public ActionResult Index()
        {
            var propertyImages = db.PropertyImages.Include(p => p.Property);
            return View(propertyImages.ToList());
        }

        // GET: PropertyImage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropertyImage propertyImage = db.PropertyImages.Find(id);
            if (propertyImage == null)
            {
                return HttpNotFound();
            }
            return View(propertyImage);
        }

        // GET: PropertyImage/Create
        public ActionResult Create()
        {
            ViewBag.PropertyId = new SelectList(db.Properties, "Id", "Name");
            return View();
        }

        // POST: PropertyImage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( PropertyImage propertyImage,HttpPostedFileBase Image)
        {
            propertyImage.Name = System.IO.Path.GetFileName(Image.FileName);
            if (ModelState.IsValid)
            {
                db.PropertyImages.Add(propertyImage);
                db.SaveChanges();
                Image.SaveAs(Server.MapPath("../Upload/PropertyImage/" + propertyImage.PropertyId.ToString() + "_" + Image.FileName));
                return RedirectToAction("Index");
            }

            ViewBag.PropertyId = new SelectList(db.Properties, "Id", "Name", propertyImage.PropertyId);
            return View(propertyImage);
        }

        // GET: PropertyImage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropertyImage propertyImage = db.PropertyImages.Find(id);
            if (propertyImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.PropertyId = new SelectList(db.Properties, "Id", "Name", propertyImage.PropertyId);
            return View(propertyImage);
        }

        // POST: PropertyImage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,PropertyId")] PropertyImage propertyImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(propertyImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PropertyId = new SelectList(db.Properties, "Id", "Name", propertyImage.PropertyId);
            return View(propertyImage);
        }

        // GET: PropertyImage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropertyImage propertyImage = db.PropertyImages.Find(id);
            if (propertyImage == null)
            {
                return HttpNotFound();
            }
            return View(propertyImage);
        }

        // POST: PropertyImage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PropertyImage propertyImage = db.PropertyImages.Find(id);
            db.PropertyImages.Remove(propertyImage);
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
