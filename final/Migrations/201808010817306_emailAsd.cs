namespace final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emailAsd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Email", c => c.String());
            DropColumn("dbo.AspNetUsers", "EmailAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "EmailAddress", c => c.String());
            DropColumn("dbo.AspNetUsers", "Email");
        }
    }
}
