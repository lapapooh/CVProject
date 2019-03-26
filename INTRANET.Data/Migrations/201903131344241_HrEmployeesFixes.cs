namespace INTRANET.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HrEmployeesFixes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HrEmployees", "ImageName", c => c.String());
            AlterColumn("dbo.HrEmployees", "ImageNameContentType", c => c.String());
            AlterColumn("dbo.HrEmployees", "ImageNameContent", c => c.Binary(maxLength: 8000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HrEmployees", "ImageNameContent", c => c.Binary(nullable: false, maxLength: 8000));
            AlterColumn("dbo.HrEmployees", "ImageNameContentType", c => c.String(nullable: false));
            AlterColumn("dbo.HrEmployees", "ImageName", c => c.String(nullable: false));
        }
    }
}
