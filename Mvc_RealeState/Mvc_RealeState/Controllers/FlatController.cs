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
    public class FlatController : Controller
    {
        private MyCon db = new MyCon();

        // GET: Flat
        public ActionResult Index()
        {
            if (Session["Type"] == null || Session["Type"].ToString() == "")
            {
                Session["dv"] = "Index";
                Session["dc"] = "Flat";
                return RedirectToAction("Login","Users");
            }
                var flats = db.Flats.Include(f => f.Discount).Include(f => f.FlatType).Include(f => f.Property);
            return View(flats.ToList());
        }

        // GET: Flat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flat flat = db.Flats.Find(id);
            if (flat == null)
            {
                return HttpNotFound();
            }
            return View(flat);
        }

        // GET: Flat/Create
        public ActionResult Create()
        {
            if (Session["Type"] == null || Session["Type"].ToString() == "")
            {
                Session["dv"] = "Create";
                Session["dc"] = "Flat";
                return RedirectToAction("Login", "Users");
            }
            ViewBag.DiscountId = new SelectList(db.Discounts, "Id", "Name");
            ViewBag.FlatTypeId = new SelectList(db.FlatTypes, "Id", "Name");
            ViewBag.PropertyId = new SelectList(db.Properties, "Id", "Name");
            return View();
        }

        // POST: Flat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Flat flat ,HttpPostedFileBase Image)
        {
            var img = System.IO.Path.GetFileName(Image.FileName);
            if (ModelState.IsValid)
            {
                db.Flats.Add(flat);
                db.SaveChanges();

                FlatImage fImage = new FlatImage();
                fImage.FlatId = flat.Id;
                fImage.Name = img;
                db.FlatImages.Add(fImage);
                db.SaveChanges();
                Image.SaveAs(Server.MapPath("../Upload/FlatImage/"+ fImage.Id.ToString()+ "_" +img));

                return RedirectToAction("Index");
            }

            ViewBag.DiscountId = new SelectList(db.Discounts, "Id", "Name", flat.DiscountId);
            ViewBag.FlatTypeId = new SelectList(db.FlatTypes, "Id", "Name", flat.FlatTypeId);
            ViewBag.PropertyId = new SelectList(db.Properties, "Id", "Name", flat.PropertyId);
            return View(flat);
        }

        // GET: Flat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flat flat = db.Flats.Find(id);
            if (flat == null)
            {
                return HttpNotFound();
            }
            ViewBag.DiscountId = new SelectList(db.Discounts, "Id", "Name", flat.DiscountId);
            ViewBag.FlatTypeId = new SelectList(db.FlatTypes, "Id", "Name", flat.FlatTypeId);
            ViewBag.PropertyId = new SelectList(db.Properties, "Id", "Name", flat.PropertyId);
            return View(flat);
        }

        // POST: Flat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Size,Total_Room,BedRoom,Kichen,WashRoom,Dining,Draining,Corridor,Position,Floor_No,Complete,Description,Sold,Price,PropertyId,DiscountId,FlatTypeId")] Flat flat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DiscountId = new SelectList(db.Discounts, "Id", "Name", flat.DiscountId);
            ViewBag.FlatTypeId = new SelectList(db.FlatTypes, "Id", "Name", flat.FlatTypeId);
            ViewBag.PropertyId = new SelectList(db.Properties, "Id", "Name", flat.PropertyId);
            return View(flat);
        }

        // GET: Flat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flat flat = db.Flats.Find(id);
            if (flat == null)
            {
                return HttpNotFound();
            }
            return View(flat);
        }

        // POST: Flat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Flat flat = db.Flats.Find(id);
            List<FlatImage> fImageList = db.FlatImages.Where(i => i.FlatId == id).ToList();
            if (fImageList.Count() > 0)
            {
                foreach (var img in fImageList)
                {
                    FlatImage pimag = new FlatImage();
                    pimag = img;
                    db.FlatImages.Remove(pimag);
                    db.SaveChanges();
                }
            }
            db.Flats.Remove(flat);
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
