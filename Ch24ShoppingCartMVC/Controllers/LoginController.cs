using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ch24ShoppingCartMVC.Models;

namespace Ch24ShoppingCartMVC.Controllers
{
    public class LoginController : Controller
    {
        private LoginContext db = new LoginContext();

        //
        // GET: /Login/

        [HttpGet]
        public ActionResult Log()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Log(User u)
        {

            bool result = db.Users.Any(X => X.Username == u.Username && X.Password == u.Password);

            if (result)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
                
            
        }


        //
        // GET: /Login/Details/5


    }
}