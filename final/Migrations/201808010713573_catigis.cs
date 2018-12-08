namespace final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class catigis : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "EmailAddress", c => c.String());
            AddColumn("dbo.AspNetUsers", "Usertype", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Usertype");
            DropColumn("dbo.AspNetUsers", "EmailAddress");
        }
    }
}
