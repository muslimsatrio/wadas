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
  





    public class UserModel
    {
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string role_id { get; set; }
        public string role_name { get; set; }
        public string user_status { get; set; }
    }

  
    


}