namespace final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyName_ADDress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.jobs", "CompanyName", c => c.String(nullable: false));
            AddColumn("dbo.jobs", "AddressCompany", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.jobs", "AddressCompany");
            DropColumn("dbo.jobs", "CompanyName");
        }
    }
}
