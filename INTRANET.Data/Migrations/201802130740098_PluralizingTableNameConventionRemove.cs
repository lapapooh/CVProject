namespace INTRANET.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PluralizingTableNameConventionRemove : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tutorials",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AcademicYearID = c.Int(nullable: false),
                        ModuleID = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        Title = c.String(),
                        CreatorID = c.String(maxLength: 128),
                        LastModifierID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYearID, cascadeDelete: true)
                .ForeignKey("dbo.Modules", t => t.ModuleID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.LastModifierID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatorID)
                .Index(t => t.AcademicYearID)
                .Index(t => t.ModuleID)
                .Index(t => t.CreatorID)
                .Index(t => t.LastModifierID);
            
            CreateTable(
                "dbo.TutorialAttachments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TutorialID = c.Int(nullable: false),
                        Name = c.String(),
                        Url = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        AuthorID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tutorials", t => t.TutorialID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorID, cascadeDelete: true)
                .Index(t => t.TutorialID)
                .Index(t => t.AuthorID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TutorialAttachments", "AuthorID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tutorials", "CreatorID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tutorials", "LastModifierID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tutorials", "ModuleID", "dbo.Modules");
            DropForeignKey("dbo.TutorialAttachments", "TutorialID", "dbo.Tutorials");
            DropForeignKey("dbo.Tutorials", "AcademicYearID", "dbo.AcademicYears");
            DropIndex("dbo.TutorialAttachments", new[] { "AuthorID" });
            DropIndex("dbo.TutorialAttachments", new[] { "TutorialID" });
            DropIndex("dbo.Tutorials", new[] { "LastModifierID" });
            DropIndex("dbo.Tutorials", new[] { "CreatorID" });
            DropIndex("dbo.Tutorials", new[] { "ModuleID" });
            DropIndex("dbo.Tutorials", new[] { "AcademicYearID" });
            DropTable("dbo.TutorialAttachments");
            DropTable("dbo.Tutorials");
        }
    }
}
