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
    public class BooKingController : Controller
    {
        private MyCon db = new MyCon();

        // GET: BooKing
        public ActionResult Index()
        {
            //if (Session["type"] == null || Session["type"].ToString() == "")
            //{
            //    Session["dv"] = "Index";
            //    Session["dc"] = "BooKing";
            //    return RedirectToAction("Login", "LoginAll");
            //}
            var buy_Sell = db.Buy_Sell.Include(b => b.Flat).Include(b => b.Payment).Include(b => b.User).Include(b => b.Voucher);
            return View(buy_Sell.ToList());
        }
        public ActionResult Bookings(int id)
        {
            if(Session["Name"]==null || Session["Name"].ToString()=="")
            {
                //Session["dv"] = "Bookings";
                //Session["dc"] = "BooKing";
                return Redirect("~/LoginAll/Login");
            }
            Booking abooking = new Booking();

            var query = (from f in db.Flats
                         join p in db.Properties on f.PropertyId equals p.Id
                         join c in db.Cities on p.CityId equals c.Id
                         where f.Id == id
                         select new Booking
                         {
                             Id = f.Id,
                             CityName = c.Name,
                             FlatName = f.Name,
                             PropertyName = p.Name,
                             Address = p.Address

                         });
            foreach (var s in query)
            {
                abooking.CityName = s.CityName;
                abooking.PropertyName = s.PropertyName;
                abooking.FlatName = s.FlatName;
                abooking.Address = s.Address;
                
            }
            abooking.UserName = Session["Name"].ToString();
            return View(abooking);
        }
        [HttpPost]
        public ActionResult Bookings(Booking aBooking)
        {
        
            if (ModelState.IsValid)
            {
                Voucher aVoucher = new Voucher();
                aVoucher.V_Discount = aBooking.Discount;
                db.Vouchers.Add(aVoucher);
                db.SaveChanges();

                Payment aPayment = new Payment();
                aPayment.Amount = aBooking.Amount;
                aPayment.Type = aBooking.Type;
                aPayment.Code = aBooking.Code;
                aPayment.VoucherId = aVoucher.Id;
                db.Payments.Add(aPayment);
                db.SaveChanges();

                Buy_Sell abuySell = new Buy_Sell();
                abuySell.FlatId = aBooking.Id;
                abuySell.UserId = Convert.ToInt32(Session["Id"].ToString());
                abuySell.VoucherId = aVoucher.Id;
                abuySell.PaymentId = aPayment.Id;
                abuySell.All_Paper = false;
                abuySell.Description = "";
                db.Buy_Sell.Add(abuySell);
                db.SaveChanges();
                TempData["message"] = "Booing Successfully";
            }
           

            return View();
        }

        // GET: BooKing/Details/5


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Buy_Sell buy_Sell = db.Buy_Sell.Find(id);
            if (buy_Sell == null)
            {
                return HttpNotFound();
            }
            return View(buy_Sell);
        }

        // GET: BooKing/Create
        public ActionResult Create()
        {
            ViewBag.FlatId = new SelectList(db.Flats, "Id", "Name");
            ViewBag.PaymentId = new SelectList(db.Payments, "Id", "Type");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            ViewBag.VoucherId = new SelectList(db.Vouchers, "Id", "Id");
            return View();
        }

        // POST: BooKing/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FlatId,UserId,VoucherId,All_Paper,Description,PaymentId")] Buy_Sell buy_Sell)
        {
            if (ModelState.IsValid)
            {
                db.Buy_Sell.Add(buy_Sell);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FlatId = new SelectList(db.Flats, "Id", "Name", buy_Sell.FlatId);
            ViewBag.PaymentId = new SelectList(db.Payments, "Id", "Type", buy_Sell.PaymentId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", buy_Sell.UserId);
            ViewBag.VoucherId = new SelectList(db.Vouchers, "Id", "Id", buy_Sell.VoucherId);
            return View(buy_Sell);
        }

        // GET: BooKing/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Buy_Sell buy_Sell = db.Buy_Sell.Find(id);
            if (buy_Sell == null)
            {
                return HttpNotFound();
            }
            ViewBag.FlatId = new SelectList(db.Flats, "Id", "Name", buy_Sell.FlatId);
            ViewBag.PaymentId = new SelectList(db.Payments, "Id", "Type", buy_Sell.PaymentId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", buy_Sell.UserId);
            ViewBag.VoucherId = new SelectList(db.Vouchers, "Id", "Id", buy_Sell.VoucherId);
            return View(buy_Sell);
        }

        // POST: BooKing/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FlatId,UserId,VoucherId,All_Paper,Description,PaymentId")] Buy_Sell buy_Sell)
        {
            if (ModelState.IsValid)
            {
                db.Entry(buy_Sell).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FlatId = new SelectList(db.Flats, "Id", "Name", buy_Sell.FlatId);
            ViewBag.PaymentId = new SelectList(db.Payments, "Id", "Type", buy_Sell.PaymentId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", buy_Sell.UserId);
            ViewBag.VoucherId = new SelectList(db.Vouchers, "Id", "Id", buy_Sell.VoucherId);
            return View(buy_Sell);
        }

        // GET: BooKing/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Buy_Sell buy_Sell = db.Buy_Sell.Find(id);
            if (buy_Sell == null)
            {
                return HttpNotFound();
            }
            return View(buy_Sell);
        }

        // POST: BooKing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Buy_Sell buy_Sell = db.Buy_Sell.Find(id);
            db.Buy_Sell.Remove(buy_Sell);
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
