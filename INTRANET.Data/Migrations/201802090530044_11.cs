namespace INTRANET.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lectures", "Module_ID", "dbo.Modules");
            DropIndex("dbo.Lectures", new[] { "Module_ID" });
            RenameColumn(table: "dbo.Lectures", name: "Module_ID", newName: "ModuleID");
            AlterColumn("dbo.Lectures", "ModuleID", c => c.Int(nullable: false));
            CreateIndex("dbo.Lectures", "ModuleID");
            AddForeignKey("dbo.Lectures", "ModuleID", "dbo.Modules", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lectures", "ModuleID", "dbo.Modules");
            DropIndex("dbo.Lectures", new[] { "ModuleID" });
            AlterColumn("dbo.Lectures", "ModuleID", c => c.Int());
            RenameColumn(table: "dbo.Lectures", name: "ModuleID", newName: "Module_ID");
            CreateIndex("dbo.Lectures", "Module_ID");
            AddForeignKey("dbo.Lectures", "Module_ID", "dbo.Modules", "ID");
        }
    }
}
