namespace INTRANET.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AwardYearNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HrCvAwards", "Year", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HrCvAwards", "Year", c => c.Int(nullable: false));
        }
    }
}
