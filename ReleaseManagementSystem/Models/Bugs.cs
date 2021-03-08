using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReleaseManagementSystem.Models
{
    public class Bugs
    {
        public string Module_Name { get; set; }
             
        [Key]
        public string Bug_Id { get; set; }
        public bool Bug_Status { get; internal set; }

        public string Bug_Description { get; internal set; }
    }
}