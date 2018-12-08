namespace final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addimageforjobs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.jobs", "image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.jobs", "image");
        }
    }
}
