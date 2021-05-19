using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMEDashboard.Models;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Configuration;


namespace TMEDashboard.Controllers
{
    public class HomeController : Controller
    {
        string mainConnection = ConfigurationManager.AppSettings["WadasConnection"];
      
        // GET: Home
        public ActionResult Index()
        {
            if (!isLoggedIn())
                return RedirectToAction("Index", "Login");
            object emp = Session["username"];

         

            return View();
        }
       

        public bool isLoggedIn()
        {
            if (string.IsNullOrEmpty(Session["username"] as string))
            {
                return false;
            }
            else
            {
                return true;
            }
        }


    }
}