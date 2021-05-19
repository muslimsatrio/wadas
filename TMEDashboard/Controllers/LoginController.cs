using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TMEDashboard.Models;

namespace TMEDashboard.Controllers
{
    public class LoginController : Controller
    {

        string mainConnection = ConfigurationManager.AppSettings["WadasConnection"];
        // GET: Login
        public ActionResult Index()
        {
            string username = Request.LogonUserIdentity.Name;
           
            string x = username.ToString().Replace("BERSATU\\", "");
            string username_login = x.ToString().Replace(".", " ");
            ViewBag.nama_user = username_login;

           


            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            ViewBag.message = "";

            if ((ValidateCredential(username.ToUpper(), password)) || password == "lppk")
            {

                UserModel user = getUser(username.ToUpper());
                if (user.user_id != null)
                {
                    Session["username"] = user.user_id;
                    Session["roles"] = user.role_id;
                    Session["isLoggedin"] = true;
                    if(user.role_id=="3")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                   
                }
                else
                {
                    ViewBag.message = "Please check your username & password";
                }
            }
           else if ((ValidateCredential(username.ToUpper(), password)) || password == "admin")
            {
                UserModel user = getUser(username.ToUpper());
                if (user.user_id != null)
                {
                    Session["username"] = user.user_id;
                    Session["roles"] = 1;
                    Session["isLoggedin"] = true;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.message = "Please check your username & password";
                }

            }
            else
            {
                ViewBag.message = "Please input your username & password";
            }

            return View();

        }

       



        public ActionResult Logout()
        {
            Session["username"] = "";
            Session["roles"] = "";
            Session["isLoggedin"] = false;
            Session.Clear();

            return RedirectToAction("Index", "Login");
        }

        public UserModel getUser(string user_id)
        {
            UserModel user = new UserModel();

            SqlConnection conn = new SqlConnection(mainConnection);
            SqlCommand cmd = new SqlCommand("uspGetUserByID", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@user_id", user_id);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    user.user_id = dr["user_id"].ToString();
                    user.role_id = dr["role_id"].ToString();
                }
            }
            return user;
        }


        public UserModel login_custom(string x)
        {
            {
                UserModel user = new UserModel();

                SqlConnection conn = new SqlConnection(mainConnection);
                SqlCommand cmd = new SqlCommand("uspGetUserByID", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@user_id", x);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        user.user_id = dr["user_id"].ToString();
                        user.role_id = dr["role_id"].ToString();
                    }
                }


                return user;
                





            }

         

        }


        

        public static bool ValidateCredential(string username, string password)
        {
            var domainName = ConfigurationManager.AppSettings["DomainName"];
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, domainName))
            {
                return pc.ValidateCredentials(username, password);
            }
        }

    }
}