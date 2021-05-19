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
    public class TransaksiController : Controller
    {
        string mainConnection = ConfigurationManager.AppSettings["WadasConnection"];
        // GET: Transaksi
        public ActionResult Index()
        {
            if (!isLoggedIn())
                return RedirectToAction("Index", "Login");

            ViewBag.transaksi = transaksiData();

            ViewBag.barang = barangData();
            ViewBag.perusahaan = perusahaanData();
            return View();
        }

        [HttpPost]
        public ActionResult transaksi_insert(string id_barang,string id_perusahaan)
        {

            SqlConnection conn = new SqlConnection(mainConnection);
            SqlCommand cmd = new SqlCommand("uspTransaksi", conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            object emp = Session["username"];
            cmd.Parameters.AddWithValue("@action", "insert_transaksi");
            cmd.Parameters.AddWithValue("@id_barang", id_barang);
            cmd.Parameters.AddWithValue("@id_perusahaan", id_perusahaan);
            cmd.Parameters.AddWithValue("@id_user", emp);
            conn.Open();

            cmd.ExecuteNonQuery();
            conn.Close();
            cmd.Dispose();




            return RedirectToAction("Index", "Transaksi");

        }

        [HttpPost]
        public ActionResult transaksi_update(string id_barang, string id_perusahaan,string id_transaksi)
        {

            SqlConnection conn = new SqlConnection(mainConnection);
            SqlCommand cmd = new SqlCommand("uspTransaksi", conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            object emp = Session["username"];
            cmd.Parameters.AddWithValue("@action", "update_transaksi");
            cmd.Parameters.AddWithValue("@id_barang", id_barang);
            cmd.Parameters.AddWithValue("@id_perusahaan", id_perusahaan);
            cmd.Parameters.AddWithValue("@id_transaksi", id_transaksi);
            conn.Open();

            cmd.ExecuteNonQuery();
            conn.Close();
            cmd.Dispose();




            return RedirectToAction("Index", "Transaksi");

        }
        [HttpPost]
        public ActionResult transaksi_delete(string id_transaksi)
        {

            SqlConnection conn = new SqlConnection(mainConnection);
            SqlCommand cmd = new SqlCommand("uspTransaksi", conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            object emp = Session["username"];
            cmd.Parameters.AddWithValue("@action", "delete_transaksi");
            
            cmd.Parameters.AddWithValue("@id_transaksi", id_transaksi);
            conn.Open();

            cmd.ExecuteNonQuery();
            conn.Close();
            cmd.Dispose();




            return RedirectToAction("Index", "Transaksi");

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
        private List<TransaksiModel> transaksiData()
        {
            List<TransaksiModel> items = new List<TransaksiModel>();

            SqlConnection conn = new SqlConnection(mainConnection);
            SqlCommand cmd = new SqlCommand("uspTransaksi", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "select_transaksi");
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    TransaksiModel item = new TransaksiModel();
                    item.id_transaksi = dr["id_transaksi"].ToString();
                    item.id_barang = dr["id_barang"].ToString();
                    item.nama_barang = dr["nama_barang"].ToString();
                    item.id_perusahaan = dr["id_perusahaan"].ToString();
                    item.nama_perusahaan = dr["nama"].ToString();
                    item.id_user = dr["id_user"].ToString();
                    item.date = dr["date"].ToString();
                   
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