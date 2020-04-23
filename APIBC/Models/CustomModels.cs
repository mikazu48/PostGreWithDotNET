using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIBC.Models
{
    public class CustomModels
    {
    }
    public class AreaData
    {
        public string AreaID { get; set; }
        public string AreaName { get; set; }
        public string Regional { get; set; }
        public string District { get; set; }
    }
    public class EmployeeData
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public char Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string ContactPhone { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public string Division { get; set; }
        public string SupervisorID { get; set; }
        public string Picture { get; set; }
    }

        public class AccountUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmployeeID { get; set; }
    }
}