namespace INTRANET.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewFieldForHint : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HrCvHintTexts", "FieldName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HrCvHintTexts", "FieldName");
        }
    }
}
