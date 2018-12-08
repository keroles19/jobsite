namespace final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplyJob : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplayForJobs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        message = c.String(),
                        ApplayDate = c.DateTime(nullable: false),
                        jobid = c.Int(nullable: false),
                        userid = c.String(),
                        UserForApply_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.jobs", t => t.jobid, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserForApply_Id)
                .Index(t => t.jobid)
                .Index(t => t.UserForApply_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplayForJobs", "UserForApply_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplayForJobs", "jobid", "dbo.jobs");
            DropIndex("dbo.ApplayForJobs", new[] { "UserForApply_Id" });
            DropIndex("dbo.ApplayForJobs", new[] { "jobid" });
            DropTable("dbo.ApplayForJobs");
        }
    }
}
