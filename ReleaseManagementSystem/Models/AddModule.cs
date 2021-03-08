using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReleaseManagementSystem.Models
{
    public class AddModule
    {
        public string Num_Ofmodules { get; set; }
        public string Project_ID { get; set; }
        public string ProjectName { get; set; }
        public string ModuleName { get; set; }
        public string Assign_Developer { get; set; }
        public string Assign_Tester { get; set; }
        public DateTime ActualStartDate { get; set; }
        public DateTime ActualEndDate { get; set; }
    }

}