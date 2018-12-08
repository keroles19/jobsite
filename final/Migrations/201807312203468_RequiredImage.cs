namespace final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredImage : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.jobs", "image", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.jobs", "image", c => c.String());
        }
    }
}
