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
    public class FlatImageController : Controller
    {
        private MyCon db = new MyCon();

        // GET: FlatImage
        public ActionResult Index()
        {
            var flatImages = db.FlatImages.Include(f => f.Flat);
            return View(flatImages.ToList());
        }

        // GET: FlatImage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlatImage flatImage = db.FlatImages.Find(id);
            if (flatImage == null)
            {
                return HttpNotFound();
            }
            return View(flatImage);
        }

        // GET: FlatImage/Create
        public ActionResult Create()
        {
            ViewBag.FlatId = new SelectList(db.Flats, "Id", "Name");
            return View();
        }

        // POST: FlatImage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FlatImage flatImage,HttpPostedFileBase Image)
        {
            flatImage.Name = System.IO.Path.GetFileName(Image.FileName);
            if (ModelState.IsValid)
            {
                db.FlatImages.Add(flatImage);
                db.SaveChanges();
                Image.SaveAs(Server.MapPath("../Upload/FlatImage/" + flatImage.Id.ToString() + "_" + Image.FileName));
                return RedirectToAction("Index");
            }

            ViewBag.FlatId = new SelectList(db.Flats, "Id", "Name", flatImage.FlatId);
            return View(flatImage);
        }

        // GET: FlatImage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlatImage flatImage = db.FlatImages.Find(id);
            if (flatImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.FlatId = new SelectList(db.Flats, "Id", "Name", flatImage.FlatId);
            return View(flatImage);
        }

        // POST: FlatImage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,FlatId")] FlatImage flatImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flatImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FlatId = new SelectList(db.Flats, "Id", "Name", flatImage.FlatId);
            return View(flatImage);
        }

        // GET: FlatImage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlatImage flatImage = db.FlatImages.Find(id);
            if (flatImage == null)
            {
                return HttpNotFound();
            }
            return View(flatImage);
        }

        // POST: FlatImage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FlatImage flatImage = db.FlatImages.Find(id);
            db.FlatImages.Remove(flatImage);
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
