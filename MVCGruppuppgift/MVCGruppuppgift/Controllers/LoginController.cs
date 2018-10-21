using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCGruppuppgift.Models;
namespace MVCGruppuppgift.Controllers
{
 

    public class LoginController : Controller
    {
        tvdbEntities db = new tvdbEntities();
        // GET: Login
        public ActionResult Login(person User)
        {
            var Usr = db.person.Where(u => u.email == User.email && u.password == User.password).FirstOrDefault();
            if (Usr == null)
            {
                return View("Login");
            }
            else
            {
                Session["id"] = Usr.personid;
                Session["email"] = Usr.email;
                Session["admin"] = Usr.role;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            int UsrId = (int)Session["id"];
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }
    }
}