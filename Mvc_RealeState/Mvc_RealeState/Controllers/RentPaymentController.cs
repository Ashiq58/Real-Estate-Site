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
    public class RentPaymentController : Controller
    {
        private MyCon db = new MyCon();

        // GET: RentPayment
        public ActionResult Index()
        {
            var rent_Payment = db.Rent_Payment.Include(r => r.Rent);
            return View(rent_Payment.ToList());
        }

        // GET: RentPayment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rent_Payment rent_Payment = db.Rent_Payment.Find(id);
            if (rent_Payment == null)
            {
                return HttpNotFound();
            }
            return View(rent_Payment);
        }

        // GET: RentPayment/Create
        public ActionResult Create()
        {
            ViewBag.RentId = new SelectList(db.Rents, "Id", "Id");
            return View();
        }

        // POST: RentPayment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RentId,Payment_Code,DataTime,Amount")] Rent_Payment rent_Payment)
        {
            if (ModelState.IsValid)
            {
                db.Rent_Payment.Add(rent_Payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RentId = new SelectList(db.Rents, "Id", "Id", rent_Payment.RentId);
            return View(rent_Payment);
        }

        // GET: RentPayment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rent_Payment rent_Payment = db.Rent_Payment.Find(id);
            if (rent_Payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.RentId = new SelectList(db.Rents, "Id", "Id", rent_Payment.RentId);
            return View(rent_Payment);
        }

        // POST: RentPayment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RentId,Payment_Code,DataTime,Amount")] Rent_Payment rent_Payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rent_Payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RentId = new SelectList(db.Rents, "Id", "Id", rent_Payment.RentId);
            return View(rent_Payment);
        }

        // GET: RentPayment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rent_Payment rent_Payment = db.Rent_Payment.Find(id);
            if (rent_Payment == null)
            {
                return HttpNotFound();
            }
            return View(rent_Payment);
        }

        // POST: RentPayment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rent_Payment rent_Payment = db.Rent_Payment.Find(id);
            db.Rent_Payment.Remove(rent_Payment);
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
