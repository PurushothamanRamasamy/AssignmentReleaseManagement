using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReleaseManagementSystem.Models
{
    public class ProjectModules
    {
        public string ProjectId { get; set; }
     
        [Key]
        public string Module_Name { get; set; }


        public string Assigned_Dev_Id { get; set; }

        public string Assigned_Tester_Id { get; set; }

        public bool Status_Dev { get; internal set; }

        public bool Status_Tester { get; internal set; }
    }
}