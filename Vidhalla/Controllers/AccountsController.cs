using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vidhalla.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Index()
        {
            return View();
        }

        // GET: Details/{id}
        public ActionResult Details(int? id, string username)
        {
            return View();
        }

        //public ActionResult Login()
        //{
            
        //}

        //public ActionResult Logout()
        //{
            
        //}

        //public ActionResult Register()
        //{
            
        //}
    }
}