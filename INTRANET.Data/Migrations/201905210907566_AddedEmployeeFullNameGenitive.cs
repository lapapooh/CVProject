namespace INTRANET.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEmployeeFullNameGenitive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HrEmployees", "FullNameGenitive", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HrEmployees", "FullNameGenitive");
        }
    }
}
