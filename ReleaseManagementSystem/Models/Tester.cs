using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReleaseManagementSystem.Models
{
    public class Tester
    {
        public string Project_Id { get; set; }
        public string Module_Name { get; set; }
        public string Assign_Developer { get; set; }
        public bool Dev_status { get; set; }
        public List<bool> Bug_status = new List<bool>();
        public List<string> Bugs_Id = new List<string>();
        public List<string> Bug_Description = new List<string>();
    }
}