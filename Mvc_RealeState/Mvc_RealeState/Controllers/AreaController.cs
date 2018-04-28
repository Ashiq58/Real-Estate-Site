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
    public class AreaController : Controller
    {
        private MyCon db = new MyCon();

        // GET: Area
        public ActionResult Index()
        {
            var properties = db.Properties.Include(p => p.City);
            return View(properties.ToList());
        }
        public ActionResult Mirpur()
        {
            IEnumerable<Propertys> qurey = (from pr in db.Properties
                                            join img in db.PropertyImages on pr.Id equals img.PropertyId
                                            //join fl in db.FlatTypes on pr.Id equals fl.Id
                                            where pr.Address== "Mirpur"
                                            select new Propertys
                                            {
                                                Id = (int)pr.Id,
                                                Name = pr.Name,
                                                Address = pr.Address,
                                                Caretecar = pr.Caretecar,
                                                //FlatType=fl.Name,
                                                CityName = pr.City.Name,
                                                Generator = pr.Generator,
                                                Description = pr.Description,
                                                Image = img.Name,
                                                Lift = pr.Lift,
                                                Parking = pr.Parking,
                                                Total_Flat = pr.Total_Flat,
                                                Total_Floor = pr.Total_Floor
                                            });
            return View(qurey.ToList());
        }
        public ActionResult Mirpur1()
        {
            IEnumerable<Propertys> qurey = (from pr in db.Properties
                                            join img in db.PropertyImages on pr.Id equals img.PropertyId
                                            //join fl in db.FlatTypes on pr.Id equals fl.Id
                                            where pr.Address == "Mirpur-1"
                                            select new Propertys
                                            {
                                                Id = (int)pr.Id,
                                                Name = pr.Name,
                                                Address = pr.Address,
                                                Caretecar = pr.Caretecar,
                                                //FlatType=fl.Name,
                                                CityName = pr.City.Name,
                                                Generator = pr.Generator,
                                                Description = pr.Description,
                                                Image = img.Name,
                                                Lift = pr.Lift,
                                                Parking = pr.Parking,
                                                Total_Flat = pr.Total_Flat,
                                                Total_Floor = pr.Total_Floor
                                            });
            return View(qurey.ToList());
        }
        public ActionResult Mirpur11()
        {
            IEnumerable<Propertys> qurey = (from pr in db.Properties
                                            join img in db.PropertyImages on pr.Id equals img.PropertyId
                                            //join fl in db.FlatTypes on pr.Id equals fl.Id
                                            where pr.Address == "Mirpur-11"
                                            select new Propertys
                                            {
                                                Id = (int)pr.Id,
                                                Name = pr.Name,
                                                Address = pr.Address,
                                                Caretecar = pr.Caretecar,
                                                //FlatType=fl.Name,
                                                CityName = pr.City.Name,
                                                Generator = pr.Generator,
                                                Description = pr.Description,
                                                Image = img.Name,
                                                Lift = pr.Lift,
                                                Parking = pr.Parking,
                                                Total_Flat = pr.Total_Flat,
                                                Total_Floor = pr.Total_Floor
                                            });
            return View(qurey.ToList());
        }
        public ActionResult Baridhara()
        {
            IEnumerable<Propertys> qurey = (from pr in db.Properties
                                            join img in db.PropertyImages on pr.Id equals img.PropertyId
                                            //join fl in db.FlatTypes on pr.Id equals fl.Id
                                            where pr.Address == "Baridhara"
                                            select new Propertys
                                            {
                                                Id = (int)pr.Id,
                                                Name = pr.Name,
                                                Address = pr.Address,
                                                Caretecar = pr.Caretecar,
                                                //FlatType=fl.Name,
                                                CityName = pr.City.Name,
                                                Generator = pr.Generator,
                                                Description = pr.Description,
                                                Image = img.Name,
                                                Lift = pr.Lift,
                                                Parking = pr.Parking,
                                                Total_Flat = pr.Total_Flat,
                                                Total_Floor = pr.Total_Floor
                                            });
            return View(qurey.ToList());
        }

        // GET: Area/Details/5
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

        // GET: Area/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name");
            return View();
        }

        // POST: Area/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Total_Flat,Total_Floor,Lift,Generator,Address,Caretecar,CityId,Parking,Description")] Property property)
        {
            if (ModelState.IsValid)
            {
                db.Properties.Add(property);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", property.CityId);
            return View(property);
        }

        // GET: Area/Edit/5
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

        // POST: Area/Edit/5
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

        // GET: Area/Delete/5
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

        // POST: Area/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Property property = db.Properties.Find(id);
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
