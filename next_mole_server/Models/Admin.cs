using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace next_mole_server.Models
{
    public class Admin
    {
        string adminEmail;
        string adminPassword;
        string adminUserName;

        public string AdminEmail { get => adminEmail; set => adminEmail = value; }
        public string AdminPassword { get => adminPassword; set => adminPassword = value; }
        public string AdminUserName { get => adminUserName; set => adminUserName = value; }
    }
}