namespace INTRANET.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class someFieldsMadeRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LectureAttachments", "AuthorID", "dbo.AspNetUsers");
            DropIndex("dbo.LectureAttachments", new[] { "AuthorID" });
            AlterColumn("dbo.LectureAttachments", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.LectureAttachments", "AuthorID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.LectureAttachments", "AuthorID");
            AddForeignKey("dbo.LectureAttachments", "AuthorID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LectureAttachments", "AuthorID", "dbo.AspNetUsers");
            DropIndex("dbo.LectureAttachments", new[] { "AuthorID" });
            AlterColumn("dbo.LectureAttachments", "AuthorID", c => c.String(maxLength: 128));
            AlterColumn("dbo.LectureAttachments", "DateCreated", c => c.DateTime());
            CreateIndex("dbo.LectureAttachments", "AuthorID");
            AddForeignKey("dbo.LectureAttachments", "AuthorID", "dbo.AspNetUsers", "Id");
        }
    }
}
