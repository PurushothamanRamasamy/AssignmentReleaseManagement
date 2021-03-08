using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReleaseManagementSystem.Models
{
    public class ApproveStatus
    {
        public string Project_ID { get; set; }
        public string ModuleName { get; set; }
        public string Assign_Developer { get; set; }
        public string Assign_Tester { get; set; }
        public bool Status_Dev { get; internal set; }

        public bool Status_Tester { get; internal set; }
        public bool Approve_Status { get; internal set; }
    }
}