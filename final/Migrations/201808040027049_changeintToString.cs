namespace final.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeintToString : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.jobs", "userid");
            RenameColumn(table: "dbo.jobs", name: "user_Id", newName: "userid");
            AlterColumn("dbo.jobs", "userid", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.jobs", "userid", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.jobs", name: "userid", newName: "user_Id");
            AddColumn("dbo.jobs", "userid", c => c.Int(nullable: false));
        }
    }
}
