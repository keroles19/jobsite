namespace final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relationJobAndUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.jobs", "userid", c => c.Int(nullable: false));
            AddColumn("dbo.jobs", "user_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.jobs", "user_Id");
            AddForeignKey("dbo.jobs", "user_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.jobs", "user_Id", "dbo.AspNetUsers");
            DropIndex("dbo.jobs", new[] { "user_Id" });
            DropColumn("dbo.jobs", "user_Id");
            DropColumn("dbo.jobs", "userid");
        }
    }
}
