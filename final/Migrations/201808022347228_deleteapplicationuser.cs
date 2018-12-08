namespace final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteapplicationuser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplayForJobs", "UserForApply_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplayForJobs", new[] { "UserForApply_Id" });
            DropColumn("dbo.ApplayForJobs", "UserForApply_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplayForJobs", "UserForApply_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.ApplayForJobs", "UserForApply_Id");
            AddForeignKey("dbo.ApplayForJobs", "UserForApply_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
