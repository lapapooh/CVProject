namespace INTRANET.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdjustedFileFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HrEmployees", "ImageNameContent", c => c.Binary());
            AlterColumn("dbo.HrEmployeeDocuments", "FileContent", c => c.Binary(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HrEmployeeDocuments", "FileContent", c => c.Binary(nullable: false, maxLength: 8000));
            AlterColumn("dbo.HrEmployees", "ImageNameContent", c => c.Binary(maxLength: 8000));
        }
    }
}
