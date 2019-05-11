namespace INTRANET.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddYearToAwards : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HrCvAwards", "Year", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HrCvAwards", "Year");
        }
    }
}
