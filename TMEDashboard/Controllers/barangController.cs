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
    public class BarangController : Controller
    {
        string mainConnection = ConfigurationManager.AppSettings["WadasConnection"];
        // GET: barang
        public ActionResult Index()
        {
            if (!isLoggedIn())
                return RedirectToAction("Index", "Login");
            ViewBag.barang = barangData();
           
            return View();
        }

        [HttpPost]
        public ActionResult barang_insert(string nama_barang)
        {

            SqlConnection conn = new SqlConnection(mainConnection);
            SqlCommand cmd = new SqlCommand("uspMaster", conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

          
            cmd.Parameters.AddWithValue("@action", "insert_barang");
            cmd.Parameters.AddWithValue("@nama_barang", nama_barang);
            conn.Open();

            cmd.ExecuteNonQuery();
            conn.Close();
            cmd.Dispose();




            return RedirectToAction("Index", "Barang");

        }
        [HttpPost]
        public ActionResult barang_update(string update_id_barang, string update_nama_barang)
        {

            SqlConnection conn = new SqlConnection(mainConnection);
            SqlCommand cmd = new SqlCommand("uspMaster", conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@action", "update_barang");
            cmd.Parameters.AddWithValue("@id_barang", update_id_barang);
            cmd.Parameters.AddWithValue("@nama_barang", update_nama_barang);
            conn.Open();

            cmd.ExecuteNonQuery();
            conn.Close();
            cmd.Dispose();




            return RedirectToAction("Index", "Barang");

        }
        [HttpPost]
        public ActionResult barang_delete(string delete_id_barang)
        {

            SqlConnection conn = new SqlConnection(mainConnection);
            SqlCommand cmd = new SqlCommand("uspMaster", conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@action", "delete_barang");
            cmd.Parameters.AddWithValue("@id_barang", delete_id_barang);
            
            conn.Open();

            cmd.ExecuteNonQuery();
            conn.Close();
            cmd.Dispose();




            return RedirectToAction("Index", "Barang");

        }

        public List<BarangModel> barangData()
        {
            List<BarangModel> items = new List<BarangModel>();

            SqlConnection conn = new SqlConnection(mainConnection);
            SqlCommand cmd = new SqlCommand("uspMaster", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "select_barang");
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    BarangModel item = new BarangModel();
                    item.id = dr["id"].ToString();
                    item.nama_barang = dr["nama_barang"].ToString();
                    item.status_barang = dr["status_barang"].ToString();
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