namespace ReleaseManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bugs",
                c => new
                    {
                        Bug_Id = c.String(nullable: false, maxLength: 128),
                        Module_Name = c.String(),
                        Bug_Status = c.Boolean(nullable: false),
                        Bug_Description = c.String(),
                    })
                .PrimaryKey(t => t.Bug_Id);
            
            CreateTable(
                "dbo.EmployeeDetails",
                c => new
                    {
                        Employee_Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        Password = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.Employee_Id);
            
            CreateTable(
                "dbo.ProjectModules",
                c => new
                    {
                        Module_Name = c.String(nullable: false, maxLength: 128),
                        ProjectId = c.String(),
                        Assigned_Dev_Id = c.String(),
                        Assigned_Tester_Id = c.String(),
                        Status_Dev = c.Boolean(nullable: false),
                        Status_Tester = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Module_Name);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Project_Id = c.String(nullable: false, maxLength: 128),
                        Project_Name = c.String(),
                        Manager_Id = c.String(),
                        TeamLead_Id = c.String(),
                        Expected_Start_Date = c.DateTime(nullable: false),
                        Expected_End_Date = c.DateTime(nullable: false),
                        Actual_Start_Date = c.DateTime(nullable: false),
                        Actual_End_Date = c.DateTime(nullable: false),
                        No_Of_Modules = c.String(),
                    })
                .PrimaryKey(t => t.Project_Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Projects");
            DropTable("dbo.ProjectModules");
            DropTable("dbo.EmployeeDetails");
            DropTable("dbo.Bugs");
        }
    }
}
