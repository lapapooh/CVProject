namespace INTRANET.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lectures", "AcademicYear_ID", "dbo.AcademicYears");
            DropIndex("dbo.Lectures", new[] { "AcademicYear_ID" });
            RenameColumn(table: "dbo.Lectures", name: "AcademicYear_ID", newName: "AcademicYearID");
            RenameColumn(table: "dbo.Lectures", name: "Creator_Id", newName: "CreatorID");
            RenameColumn(table: "dbo.Lectures", name: "LastModifier_Id", newName: "LastModifierID");
            RenameIndex(table: "dbo.Lectures", name: "IX_Creator_Id", newName: "IX_CreatorID");
            RenameIndex(table: "dbo.Lectures", name: "IX_LastModifier_Id", newName: "IX_LastModifierID");
            AlterColumn("dbo.Lectures", "AcademicYearID", c => c.Int(nullable: false));
            CreateIndex("dbo.Lectures", "AcademicYearID");
            AddForeignKey("dbo.Lectures", "AcademicYearID", "dbo.AcademicYears", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lectures", "AcademicYearID", "dbo.AcademicYears");
            DropIndex("dbo.Lectures", new[] { "AcademicYearID" });
            AlterColumn("dbo.Lectures", "AcademicYearID", c => c.Int());
            RenameIndex(table: "dbo.Lectures", name: "IX_LastModifierID", newName: "IX_LastModifier_Id");
            RenameIndex(table: "dbo.Lectures", name: "IX_CreatorID", newName: "IX_Creator_Id");
            RenameColumn(table: "dbo.Lectures", name: "LastModifierID", newName: "LastModifier_Id");
            RenameColumn(table: "dbo.Lectures", name: "CreatorID", newName: "Creator_Id");
            RenameColumn(table: "dbo.Lectures", name: "AcademicYearID", newName: "AcademicYear_ID");
            CreateIndex("dbo.Lectures", "AcademicYear_ID");
            AddForeignKey("dbo.Lectures", "AcademicYear_ID", "dbo.AcademicYears", "ID");
        }
    }
}
