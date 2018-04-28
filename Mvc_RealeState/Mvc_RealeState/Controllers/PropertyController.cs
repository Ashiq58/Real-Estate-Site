using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mvc_RealeState.Models;
using System.Web.Helpers;

namespace Mvc_RealeState.Controllers
{
    public class PropertyController : Controller
    {
        private MyCon db = new MyCon();

        // GET: Property
        public ActionResult Index()
        {
            var properties = db.Properties.Include(p => p.City);
            return View(properties.ToList());
        }

        // GET: Property/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // GET: Property/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name");
            return View();
        }

        // POST: Property/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Property property ,HttpPostedFileBase Image)
        {
            var img = System.IO.Path.GetFileName(Image.FileName);
            if (ModelState.IsValid)
            {
                db.Properties.Add(property);
                db.SaveChanges();
                PropertyImage pImage = new PropertyImage();
                pImage.PropertyId = property.Id;
                pImage.Name = img;
                db.PropertyImages.Add(pImage);
                db.SaveChanges();
                //WebImage primg = new WebImage(Image.InputStream);
                //primg.Resize(400, 400);
                Image.SaveAs(Server.MapPath("../Upload/PropertyImage/"+ property.Id.ToString()+"_"+ img));
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", property.CityId);
            return View(property);
        }

        // GET: Property/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", property.CityId);
            return View(property);
        }

        // POST: Property/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Total_Flat,Total_Floor,Lift,Generator,Address,Caretecar,CityId,Parking,Description")] Property property)
        {
            if (ModelState.IsValid)
            {
                db.Entry(property).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", property.CityId);
            return View(property);
        }

        // GET: Property/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // POST: Property/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Property property = db.Properties.Find(id);
            List<PropertyImage> pImageList = db.PropertyImages.Where(i => i.PropertyId == id).ToList();
            if(pImageList.Count()>0)
            {
                foreach(var img in pImageList)
                {
                    PropertyImage pimag = new PropertyImage();
                    pimag = img;
                    db.PropertyImages.Remove(pimag);
                    db.SaveChanges();
                }
            }
            db.Properties.Remove(property);
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
