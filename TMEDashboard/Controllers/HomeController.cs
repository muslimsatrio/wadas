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

            ViewBag.count = countData();

            return View();
        }


        private TransaksiModel countData()
        {
            TransaksiModel Transaksi = new TransaksiModel();
            SqlConnection conn = new SqlConnection(mainConnection);
            SqlCommand cmd = new SqlCommand("uspTransaksi", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "count_transaksi_day");
            
            conn.Open();
            Transaksi.id_barang = cmd.ExecuteScalar().ToString();


            conn.Close();

            return Transaksi;
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