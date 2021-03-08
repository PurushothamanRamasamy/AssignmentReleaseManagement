using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReleaseManagementSystem.Models
{
    public class ViewProjectbyTeamLeader
    {
        public string Project_Id{ get; set; }
        public string ProjectName { get; set; }
        public string TeamLead { get; set; }
        public  DateTime ExpectedStartDate { get; set; }

        public DateTime ExpectedEndDate { get; set; }
    }
}