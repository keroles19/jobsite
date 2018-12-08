namespace final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class startup : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ApplayForJobs", "userid", c => c.String(maxLength: 128));
            CreateIndex("dbo.ApplayForJobs", "userid");
            AddForeignKey("dbo.ApplayForJobs", "userid", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplayForJobs", "userid", "dbo.AspNetUsers");
            DropIndex("dbo.ApplayForJobs", new[] { "userid" });
            AlterColumn("dbo.ApplayForJobs", "userid", c => c.String());
        }
    }
}
