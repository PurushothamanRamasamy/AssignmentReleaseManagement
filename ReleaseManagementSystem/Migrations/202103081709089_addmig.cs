﻿namespace ReleaseManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmig : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bugs", "Bug_Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bugs", "Bug_Status", c => c.Int(nullable: false));
        }
    }
}