using Mvc_RealeState.Models;
using Mvc_RealeState.NewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Mvc_RealeState.Controllers
{
    public class ClientController : Controller
    {
        MyCon db = new MyCon();
        
        public ActionResult Home()
        {
            IEnumerable<Propertys> qurey = (from pr in db.Properties
                                           join img in db.PropertyImages on pr.Id equals img.PropertyId
                                           //join fl in db.FlatTypes on pr.Id equals fl.Id
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
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int propId =(int) id;
            List<Flat> Flats = (from flt in db.Flats
                                       where flt.PropertyId.Equals(propId) 
                                       select flt).ToList();

            List<FlatImage> FlatImages = (from fimg in db.FlatImages
                                        select fimg).ToList();
            ViewBag.FlatImages = FlatImages;


            ViewBag.flats = Flats;
            
            return View();                
        }
        public ActionResult DetailsRent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int propId = (int)id;
            List<Flat> Flats = (from flt in db.Flats
                                where flt.PropertyId.Equals(propId)
                                select flt).ToList();

            List<FlatImage> FlatImages = (from fimg in db.FlatImages
                                          select fimg).ToList();
            ViewBag.FlatImages = FlatImages;


            ViewBag.flats = Flats;

            return View();
        }
        public ActionResult DetailsBuy(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int propId = (int)id;
            List<Flat> Flats = (from flt in db.Flats
                                where flt.PropertyId.Equals(propId)
                                select flt).ToList();

            List<FlatImage> FlatImages = (from fimg in db.FlatImages
                                          select fimg).ToList();
            ViewBag.FlatImages = FlatImages;


            ViewBag.flats = Flats;

            return View();
        }


        public ActionResult About()
        {
            return View();
        }
        
        public ActionResult Contact()
        {

            return View();
        }
        public ActionResult Rent()
        {   
            IEnumerable<Propertys> qurey = (from pr in db.Properties
                                            join img in db.PropertyImages on pr.Id equals img.PropertyId
                                            join fl in db.Flats on pr.Id equals fl.PropertyId
                                            where fl.FlatTypeId == 1
                                            //join fl in db.FlatTypes on pr.Id equals fl.Id
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
                                            }).Distinct();
            return View(qurey.ToList());
        }
        public ActionResult Buy()
        {
            var aFlat = db.Flats.FirstOrDefault();
            
            var qurey = (from pr in db.Properties
                                            join img in db.PropertyImages on pr.Id equals img.PropertyId
                                            join fl in db.Flats on pr.Id equals fl.PropertyId
                                            where fl.FlatTypeId == 2
                                            //join fl in db.FlatTypes on pr.Id equals fl.Id
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
                                            }).Distinct();
            return View(qurey.ToList());
        }

        // GET: Queries/Create
        public ActionResult QureyCreate()
        {
            return View();
        }

        // POST: Queries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QureyCreate([Bind(Include = "Id,Cell,Email,Name,Message")] Query query)
        {
            if (ModelState.IsValid)
            {
                db.Queries.Add(query);
                db.SaveChanges();
                return RedirectToAction("Index");
                
            }

            return View(query);
        }
        [HttpPost]
        public ActionResult Contact( Query query)
        {
            if (ModelState.IsValid)
            {
                db.Queries.Add(query);
                db.SaveChanges();
                TempData["message"] = "Sent Successfully";
                return RedirectToAction("Contact");

            }

            return View(query);
        }

    }
}