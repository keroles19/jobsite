namespace final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileToJobsModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplayForJobs", "Cv", c => c.String(nullable: false));
            DropColumn("dbo.ApplayForJobs", "message");
           
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CvViewModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(nullable: false),
                        Age = c.String(nullable: false),
                        PponeNumber = c.String(),
                        Cv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ApplayForJobs", "message", c => c.String());
            DropColumn("dbo.ApplayForJobs", "Cv");
        }
    }
}
