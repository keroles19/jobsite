namespace final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fcolumn : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Catigs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Details = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.jobs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        details = c.String(),
                        catigid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Catigs", t => t.catigid, cascadeDelete: true)
                .Index(t => t.catigid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.jobs", "catigid", "dbo.Catigs");
            DropIndex("dbo.jobs", new[] { "catigid" });
            DropTable("dbo.jobs");
            DropTable("dbo.Catigs");
        }
    }
}
