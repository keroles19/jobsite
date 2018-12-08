namespace final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class memtype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "memType", c => c.String());
            DropColumn("dbo.AspNetUsers", "Usertype");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Usertype", c => c.String());
            DropColumn("dbo.AspNetUsers", "memType");
        }
    }
}
