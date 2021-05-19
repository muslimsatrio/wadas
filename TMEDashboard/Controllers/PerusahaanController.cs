using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMEDashboard.Models;

namespace LAMPDashboard.Controllers
{
    public class PerusahaanController : Controller
    {
        string mainConnection = ConfigurationManager.AppSettings["WadasConnection"];
        // GET: Perusahaan
        public ActionResult Index()
        {
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