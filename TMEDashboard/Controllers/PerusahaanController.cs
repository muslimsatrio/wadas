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
            if (!isLoggedIn())
                return RedirectToAction("Index", "Login");
            ViewBag.perusahaan = perusahaanData();
            return View();
        }


        [HttpPost]
        public ActionResult perusahaan_insert(string nama_perusahaan,string alamat_perusahaan)
        {

            SqlConnection conn = new SqlConnection(mainConnection);
            SqlCommand cmd = new SqlCommand("uspMaster", conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@action", "insert_perusahaan");
            cmd.Parameters.AddWithValue("@nama_perusahaan", nama_perusahaan);
            cmd.Parameters.AddWithValue("@alamat", alamat_perusahaan);
            conn.Open();

            cmd.ExecuteNonQuery();
            conn.Close();
            cmd.Dispose();




            return RedirectToAction("Index", "Perusahaan");

        }
        [HttpPost]
        public ActionResult perusahaan_update(string update_nama_perusahaan, string update_alamat_perusahaan, string update_id_perusahaan)
        {

            SqlConnection conn = new SqlConnection(mainConnection);
            SqlCommand cmd = new SqlCommand("uspMaster", conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@action", "update_perusahaan");
            cmd.Parameters.AddWithValue("@id_perusahaan", update_id_perusahaan);
            cmd.Parameters.AddWithValue("@nama_perusahaan", update_nama_perusahaan);
            cmd.Parameters.AddWithValue("@alamat", update_alamat_perusahaan);
          
            conn.Open();

            cmd.ExecuteNonQuery();
            conn.Close();
            cmd.Dispose();




            return RedirectToAction("Index", "Perusahaan");

        }
        [HttpPost]
        public ActionResult perusahaan_delete(string delete_id_perusahaan)
        {

            SqlConnection conn = new SqlConnection(mainConnection);
            SqlCommand cmd = new SqlCommand("uspMaster", conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@action", "delete_perusahaan");
            cmd.Parameters.AddWithValue("@id_perusahaan", delete_id_perusahaan);

            conn.Open();

            cmd.ExecuteNonQuery();
            conn.Close();
            cmd.Dispose();




            return RedirectToAction("Index", "Perusahaan");

        }

        public List<PerusahaanModel> perusahaanData()
        {
            List<PerusahaanModel> items = new List<PerusahaanModel>();

            SqlConnection conn = new SqlConnection(mainConnection);
            SqlCommand cmd = new SqlCommand("uspMaster", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "select_perusahaan");
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    PerusahaanModel item = new PerusahaanModel();
                    item.id = dr["id"].ToString();
                    item.nama = dr["nama"].ToString();
                    item.alamat = dr["alamat"].ToString();
                    items.Add(item);
                }
            }
            conn.Close();

            return items;
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