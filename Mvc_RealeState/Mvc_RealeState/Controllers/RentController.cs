using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mvc_RealeState.Models;
using Mvc_RealeState.NewModel;

namespace Mvc_RealeState.Controllers
{
    public class RentController : Controller
    {
        private MyCon db = new MyCon();

        // GET: Rent
        public ActionResult Index()
        {
            IEnumerable<RentPaymentInfo> qurey = (from RP in db.Rent_Payment
                                                  join R in db.Rents on RP.RentId equals R.Id
                                                  join F in db.Flats on R.FlatId equals F.Id
                                                  join U in db.Users on R.UserId equals U.Id
                                                  select new RentPaymentInfo
                                                  {
                                                      DateTime = RP.DataTime.ToString(),
                                                      Payment_Code = RP.Payment_Code.ToString(),
                                                      Amount = RP.Amount,
                                                      FlatId = F.Id,
                                                      UserId = U.Id,
                                                      Contact = R.Contact.ToString(),
                                                      FlatName = F.Name,
                                                      UserName = U.Name,
                                                  });
            return View(qurey.ToList());
        }

        // GET: Rent/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rent rent = db.Rents.Find(id);
            if (rent == null)
            {
                return HttpNotFound();
            }
            return View(rent);
        }

        // GET: Rent/Create
        public ActionResult Create()
        {
            ViewBag.FlatId = new SelectList(db.Flats, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: Rent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FlatId,UserId,Price,Paper,Contact")] Rent rent)
        {
            if (ModelState.IsValid)
            {
                db.Rents.Add(rent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FlatId = new SelectList(db.Flats, "Id", "Name", rent.FlatId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", rent.UserId);
            return View(rent);
        }

        // GET: Rent/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rent rent = db.Rents.Find(id);
            if (rent == null)
            {
                return HttpNotFound();
            }
            ViewBag.FlatId = new SelectList(db.Flats, "Id", "Name", rent.FlatId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", rent.UserId);
            return View(rent);
        }

        // POST: Rent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FlatId,UserId,Price,Paper,Contact")] Rent rent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FlatId = new SelectList(db.Flats, "Id", "Name", rent.FlatId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", rent.UserId);
            return View(rent);
        }

        // GET: Rent/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rent rent = db.Rents.Find(id);
            if (rent == null)
            {
                return HttpNotFound();
            }
            return View(rent);
        }

        // POST: Rent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rent rent = db.Rents.Find(id);
            db.Rents.Remove(rent);
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
