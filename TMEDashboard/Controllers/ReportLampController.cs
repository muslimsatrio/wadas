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
using System.Net.Mail;
using LAMPDashboard.Repo;
using System.Globalization;

namespace LAMPDashboard.Controllers
{
    public class ReportLampController : Controller

    {
        string mainConnection = ConfigurationManager.AppSettings["LAMConnection"];
        // GET: ReportAdmin
        public ActionResult Index()
        {
            if (!isLoggedIn())
                return RedirectToAction("Index", "Login");
            object emp = Session["username"];
            ViewBag.UserMatrix = usermatrixData(emp);
            return View();
        }
        public ActionResult detailreportlamp(string id, string area, string brand, string lamp)
        {
            if (!isLoggedIn())
                return RedirectToAction("Index", "Login");
            ViewBag.UserMatrix = Selectreportid(id, area, brand, lamp);
            ViewBag.UserActivation = Selectactivationtid(id, area, brand, lamp);
            return View();
        }
        private List<UserMatrixModel> usermatrixData(object emp)
        {
            List<UserMatrixModel> matrixs = new List<UserMatrixModel>();

            SqlConnection conn = new SqlConnection(mainConnection);
            SqlCommand cmd = new SqlCommand("uspMatrix", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "select_report_lamp");
            cmd.Parameters.AddWithValue("@lamp", emp);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    UserMatrixModel matrix = new UserMatrixModel();
                    matrix.user_matrix_id = dr["user_matrix_id"].ToString();
                    matrix.tahun = dr["tahun"].ToString();
                    matrix.budget = dr["budget"].ToString();
                    matrix.lamp = dr["lamp"].ToString();
                    matrix.head_lamp_name = dr["head_lamp_name"].ToString();
                    matrix.area = dr["area"].ToString();
                    matrix.brand = dr["brand"].ToString();
                    matrix.brand_manager = dr["brand_manager"].ToString();
                    matrix.category_head = dr["category_head"].ToString();
                    matrix.lamp_name = dr["lamp_name"].ToString();
                    matrix.area_name = dr["area_name"].ToString();
                    matrix.brand_name = dr["brand_name"].ToString();
                    matrix.brand_manager_name = dr["brand_manager_name"].ToString();
                    matrix.category_head_name = dr["category_head_name"].ToString();
                    matrix.matrix_status = dr["matrix_status"].ToString();
                    matrix.budget_terpakai = dr["budget_terpakai"].ToString();
                    matrix.budget_tersedia = dr["budget_tersedia"].ToString();

                    string area = dr["area"].ToString();
                    string brand = dr["brand"].ToString();
                    string lamp = dr["lamp"].ToString();

                    SqlConnection connDetail = new SqlConnection(mainConnection);
                    SqlCommand cmdDetail = new SqlCommand("uspMatrix", connDetail);
                    cmdDetail.CommandType = System.Data.CommandType.StoredProcedure;
                    cmdDetail.Parameters.AddWithValue("@action", "select_activation");
                    cmdDetail.Parameters.AddWithValue("@area", area);
                    cmdDetail.Parameters.AddWithValue("@brand", brand);
                    cmdDetail.Parameters.AddWithValue("@lamp", lamp);
                    connDetail.Open();
                    SqlDataReader drDetail = cmdDetail.ExecuteReader();
                    List<ActivationModel> listActivation = new List<ActivationModel>();
                    if (drDetail.HasRows)
                    {
                        while (drDetail.Read())
                        {
                            ActivationModel activation = new ActivationModel();
                            activation.activation_number = drDetail["activation_number"].ToString();
                            activation.activation_budget = drDetail["activation_budget"].ToString();
                            activation.activation_end_date = drDetail["activation_end_date"].ToString();
                            listActivation.Add(activation);
                        }
                    }
                    connDetail.Close();
                    matrix.List_Activation = listActivation;



                    matrixs.Add(matrix);
                }
            }
            conn.Close();

            return matrixs;
        }
        private List<UserMatrixModel> Selectreportid(string id, string area, string brand, string lamp)
        {
            List<UserMatrixModel> matrixs = new List<UserMatrixModel>();

            SqlConnection conn = new SqlConnection(mainConnection);
            SqlCommand cmd = new SqlCommand("uspMatrix", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "select_report_admin_id");
            cmd.Parameters.AddWithValue("@user_matrix_id", id);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    UserMatrixModel matrix = new UserMatrixModel();
                    matrix.user_matrix_id = dr["user_matrix_id"].ToString();
                    matrix.tahun = dr["tahun"].ToString();
                    matrix.budget = dr["budget"].ToString();
                    matrix.lamp = dr["lamp"].ToString();
                    matrix.head_lamp_name = dr["head_lamp_name"].ToString();
                    matrix.area = dr["area"].ToString();
                    matrix.brand = dr["brand"].ToString();
                    matrix.brand_manager = dr["brand_manager"].ToString();
                    matrix.category_head = dr["category_head"].ToString();
                    matrix.lamp_name = dr["lamp_name"].ToString();
                    matrix.area_name = dr["area_name"].ToString();
                    matrix.brand_name = dr["brand_name"].ToString();
                    matrix.brand_manager_name = dr["brand_manager_name"].ToString();
                    matrix.category_head_name = dr["category_head_name"].ToString();
                    matrix.matrix_status = dr["matrix_status"].ToString();
                    matrix.budget_terpakai = dr["budget_terpakai"].ToString();
                    matrix.budget_tersedia = dr["budget_tersedia"].ToString();
                    matrixs.Add(matrix);
                }
            }
            conn.Close();

            return matrixs;
        }
        private List<ActivationModel> Selectactivationtid(string id, string area, string brand, string lamp)
        {
            List<ActivationModel> matrixs = new List<ActivationModel>();

            SqlConnection conn = new SqlConnection(mainConnection);
            SqlCommand cmd = new SqlCommand("uspMatrix", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "select_activation");
            cmd.Parameters.AddWithValue("@area", area);
            cmd.Parameters.AddWithValue("@brand", brand);
            cmd.Parameters.AddWithValue("@lamp", lamp);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ActivationModel matrix = new ActivationModel();
                    matrix.activation_number = dr["activation_number"].ToString();
                    matrix.activation_name = dr["activation_name"].ToString();
                    matrix.activation_budget = dr["activation_budget"].ToString();
                    matrix.activation_start_date = dr["activation_start_date"].ToString();
                    matrix.activation_end_date = dr["activation_end_date"].ToString();


                    matrixs.Add(matrix);
                }
            }
            conn.Close();

            return matrixs;
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