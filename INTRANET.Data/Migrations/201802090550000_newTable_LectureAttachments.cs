namespace INTRANET.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newTable_LectureAttachments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LectureAttachments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LectureID = c.Int(nullable: false),
                        Name = c.String(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Lectures", t => t.LectureID, cascadeDelete: true)
                .Index(t => t.LectureID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LectureAttachments", "LectureID", "dbo.Lectures");
            DropIndex("dbo.LectureAttachments", new[] { "LectureID" });
            DropTable("dbo.LectureAttachments");
        }
    }
}
