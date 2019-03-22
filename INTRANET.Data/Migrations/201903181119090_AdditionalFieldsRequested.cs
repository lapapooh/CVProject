namespace INTRANET.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdditionalFieldsRequested : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HrEmployees", "PassportNo", c => c.String());
            AddColumn("dbo.HrEmployees", "PassportIssueDate", c => c.DateTime());
            AddColumn("dbo.HrEmployees", "PassportIssuePlace", c => c.String());
            AddColumn("dbo.HrEmployees", "CreatedBy", c => c.Int());
            AddColumn("dbo.HrEmployees", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.HrEmployees", "ModifiedBy", c => c.Int());
            AddColumn("dbo.HrEmployees", "ModifiedAt", c => c.DateTime());
            AddColumn("dbo.HrDepartments", "CreatedBy", c => c.Int());
            AddColumn("dbo.HrDepartments", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.HrDepartments", "ModifiedBy", c => c.Int());
            AddColumn("dbo.HrDepartments", "ModifiedAt", c => c.DateTime());
            AddColumn("dbo.HrPositions", "CreatedBy", c => c.Int());
            AddColumn("dbo.HrPositions", "CreatedAt", c => c.DateTime());
            AddColumn("dbo.HrPositions", "ModifiedBy", c => c.Int());
            AddColumn("dbo.HrPositions", "ModifiedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HrPositions", "ModifiedAt");
            DropColumn("dbo.HrPositions", "ModifiedBy");
            DropColumn("dbo.HrPositions", "CreatedAt");
            DropColumn("dbo.HrPositions", "CreatedBy");
            DropColumn("dbo.HrDepartments", "ModifiedAt");
            DropColumn("dbo.HrDepartments", "ModifiedBy");
            DropColumn("dbo.HrDepartments", "CreatedAt");
            DropColumn("dbo.HrDepartments", "CreatedBy");
            DropColumn("dbo.HrEmployees", "ModifiedAt");
            DropColumn("dbo.HrEmployees", "ModifiedBy");
            DropColumn("dbo.HrEmployees", "CreatedAt");
            DropColumn("dbo.HrEmployees", "CreatedBy");
            DropColumn("dbo.HrEmployees", "PassportIssuePlace");
            DropColumn("dbo.HrEmployees", "PassportIssueDate");
            DropColumn("dbo.HrEmployees", "PassportNo");
        }
    }
}
