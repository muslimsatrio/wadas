using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TMEDashboard.Models
{
    public class ApplicationModels
    {

    }

    public class BaseResponse{
        public bool status { get; set; }
    }

    public class RoleModel {
        public string role_id { get; set; }
        public string role_name { get; set; }
        public string role_status { get; set; }
    }

    public class BarangModel
    {
        public string id { get; set; }
        public string nama_barang { get; set; }
        public string status_barang { get; set; }
    }
    public class PerusahaanModel
    {
        public string id { get; set; }
        public string nama { get; set; }
        public string alamat { get; set; }
        public string status { get; set; }
    }
    public class TransaksiModel
    {
        public string id_transaksi { get; set; }
        public string id_barang { get; set; }
        public string id_perusahaan { get; set; }
        public string id_user { get; set; }
        public string date { get; set; }
        public string status { get; set; }
        public List<BarangModel> List_Barang{ get; set; }
        public List<PerusahaanModel> List_perusahaan { get; set; }
      
    }





    public class UserModel
    {
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string role_id { get; set; }
        public string role_name { get; set; }
        public string user_status { get; set; }
    }

  
    


}