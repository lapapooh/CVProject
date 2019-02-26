namespace INTRANET.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LectureAttachmentNewFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LectureAttachments", "DateCreated", c => c.DateTime());
            AddColumn("dbo.LectureAttachments", "AuthorID", c => c.String(maxLength: 128));
            CreateIndex("dbo.LectureAttachments", "AuthorID");
            AddForeignKey("dbo.LectureAttachments", "AuthorID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LectureAttachments", "AuthorID", "dbo.AspNetUsers");
            DropIndex("dbo.LectureAttachments", new[] { "AuthorID" });
            DropColumn("dbo.LectureAttachments", "AuthorID");
            DropColumn("dbo.LectureAttachments", "DateCreated");
        }
    }
}
