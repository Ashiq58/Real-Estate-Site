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
using Mvc_RealeState.NewModel.ViewModel;
using System.Web.Security;

namespace Mvc_RealeState.Controllers
{
    public class LoginAllController : Controller
    {
        MyCon db = new MyCon();

        public ActionResult Login()
        {
            //if (Session["type"] == null || Session["type"].ToString() == "")
            //{
            //    Session["dv"] = "Login";
            //    Session["dc"] = "LoginAll";
            //    return RedirectToAction("Login");
            //}
            ViewBag.CityId = db.Cities.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Login(ViewLogin logModel)
        {
            LoginModel login = logModel.Login;

            if (ModelState.IsValid)
            {
                User user = new User();
                user = db.Users.FirstOrDefault(u => u.Email == login.Email);
                if (user.UserType.Name != null && user.UserType.Name != "")
                {
                    if (user.UserType.Name == "Admin")
                    {
                        if (user.Email == login.Email && user.Password == login.Password)
                        {
                            Session["Id"] = user.Id;
                            Session["Name"] = user.Name;
                            Session["Type"] = "Admin";
                           
                                return RedirectToAction("Index", "Home");
                           
                           
                        }
                        else
                        {
                            ViewBag.msg = "Email or Password Does not match";
                        }
                    }
                    else if (user.UserType.Name == "User")
                    {
                        if (user.Email == login.Email && user.Password == login.Password)
                        {
                            Session["Id"] = user.Id;
                            Session["Name"] = user.Name;
                            Session["Type"] = "User";
                            return RedirectToAction("Home", "Client");   
                           

                        }
                        else
                        {
                            ViewBag.msg = "Email or Password Does not match";
                        }

                    }
                }
                else
                {
                    ViewBag.msg = "You are not authorized";
                }                              
                
            }
            ViewBag.CityId = db.Cities.ToList();
            return View();
        }


        [HttpPost]
        public ActionResult Register(ViewLogin reg)
        {
            User userReg = reg.UserReg;
            userReg.UserTypeId = 2;
            
            if (ModelState.IsValid)
            {
                
                db.Users.Add(userReg);

                db.SaveChanges();
                TempData["message"] = "Registered Successfully";
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.CityId = db.Cities.ToList();
                TempData["message"] = "Required some value";
                return RedirectToAction("Login", reg.UserReg);
            }

            
        }

        
        public ActionResult Logout()
        {
            Session["Id"] = "";
            Session["Name"] = "";
            Session["Type"] = "";
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return Redirect("~/LoginAll/Login");
        }
        //public ActionResult LoginPublic()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult LoginPublic(Models.LoginModel loginModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var usr = db.Users.Where(u => u.Email.ToLower() == loginModel.Email.ToLower() && u.Password == loginModel.Password).First();
        //        if (usr == null)
        //        {
        //            ViewBag.error = "Invalide log in";
        //        }
        //        else
        //        {
        //            Session["Id"] = usr.Id;
        //            Session["Name"] = usr.Name;
        //            Session["Type"] = "User";

        //            if (Session["dv"] == null || Session["dv"].ToString() == "")
        //            {
        //                return RedirectToAction("Index", "Home");
        //            }
        //            return RedirectToAction(Session["dv"].ToString(), Session["dc"].ToString());
        //        }
        //    }
        //    return View(loginModel);
        //}
        //public ActionResult LogoutPublic()
        //{
        //    Session["Id"] = "";
        //    Session["Name"] = "";
        //    Session["Type"] = "";
        //    return View();
        //}


    }
}