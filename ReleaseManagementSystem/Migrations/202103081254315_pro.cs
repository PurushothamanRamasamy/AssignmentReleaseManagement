namespace ReleaseManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pro : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Bugs");
            AddColumn("dbo.Bugs", "Project_Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Bugs", "Project_Id");
            DropColumn("dbo.Bugs", "Bug_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bugs", "Bug_Id", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.Bugs");
            DropColumn("dbo.Bugs", "Project_Id");
            AddPrimaryKey("dbo.Bugs", "Bug_Id");
        }
    }
}
