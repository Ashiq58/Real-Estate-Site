using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Mvc_RealeState.Models;

namespace Mvc_RealeState.Controllers
{
    public class HomeController : Controller
    {
        private MyCon db = new MyCon();

        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult Admin()
        {

            return View();
        }



        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


       
    }
}