using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ReleaseManagementSystem.Models
{
    public class ReleaseManagementContext:DbContext
    {
        public ReleaseManagementContext():base("ConStr")
        {

        }
        public DbSet<EmployeeDetails> EmployeeDetails { get; set; }
        public DbSet<ProjectModules> ProjectModules { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Bugs> Bugs { get; set; }
    }
}