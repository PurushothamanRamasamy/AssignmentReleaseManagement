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
        [Display(Name = "Project Id")]
        public string ProjectId { get; set; }

        [Display(Name = "Module Name")]
        [Key]
        public string Module_Name { get; set; }

        [Display(Name = "Assign Developer")]
        public string Assigned_Dev_Id { get; set; }

        [Display(Name = "Assign Tester")]
        public string Assigned_Tester_Id { get; set; }

        [Display(Name = "Developer Status")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You have to tick the box!")]
        public bool Status_Dev { get;  set; }

        [Display(Name = "Tester Status")]
        public bool Status_Tester { get;  set; }

        [Display(Name = "Approve Module Status")]
        public bool Approve_Status { get; internal set; }
    }
}