namespace final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRequiredimage : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.jobs", "image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.jobs", "image", c => c.String(nullable: false));
        }
    }
}
