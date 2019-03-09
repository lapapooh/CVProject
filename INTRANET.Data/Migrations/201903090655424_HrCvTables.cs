namespace INTRANET.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HrCvTables : DbMigration
    {
        public override void Up()
        {

            CreateTable(
                "dbo.HrCvAwards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HrCvDetailId = c.Int(nullable: false),
                        Award = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HrCvDetails", t => t.HrCvDetailId, cascadeDelete: true)
                .Index(t => t.HrCvDetailId);
            
            CreateTable(
                "dbo.HrCvDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        Language = c.Int(nullable: false),
                        PlaceOfBirth = c.String(),
                        Nationality = c.String(),
                        PartyMembership = c.String(),
                        EducationDegree = c.String(),
                        EducationSpeciality = c.String(),
                        AcademicDegree = c.String(),
                        AcademicTitle = c.String(),
                        Languages = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HrEmployees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.HrCvEductions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HrCvDetailId = c.Int(nullable: false),
                        Education = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HrCvDetails", t => t.HrCvDetailId, cascadeDelete: true)
                .Index(t => t.HrCvDetailId);
            
            CreateTable(
                "dbo.HrEmployees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        Code_1C = c.String(nullable: false),
                        ID_1C = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        PlaceOfBirth = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        EmailLogin = c.String(nullable: false),
                        EntryDate = c.DateTime(nullable: false),
                        LeaveDate = c.DateTime(),
                        PositionId = c.Int(),
                        DepartmentId = c.Int(),
                        PositionStartDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        ComplietedRuCv = c.Boolean(nullable: false),
                        ComplietedUzCv = c.Boolean(nullable: false),
                        ImageName = c.String(nullable: false),
                        ImageNameContentType = c.String(nullable: false),
                        ImageNameContent = c.Binary(nullable: false, maxLength: 8000),
                        PhoneNo = c.String(),
                        ExternalPhoneNo = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HrDepartments", t => t.DepartmentId)
                .ForeignKey("dbo.HrPositions", t => t.PositionId)
                .Index(t => t.PositionId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.HrDepartments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code_1C = c.String(nullable: false),
                        TitleUz = c.String(),
                        TitleRu = c.String(),
                        TitleEn = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HrEmployeeDocuments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        FileName = c.String(nullable: false),
                        FileContentType = c.String(nullable: false),
                        FileContent = c.Binary(nullable: false, maxLength: 8000),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HrEmployees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.HrPositions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code_1C = c.String(nullable: false),
                        TitleUz = c.String(),
                        TitleRu = c.String(),
                        TitleEn = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HrCvLabors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HrCvDetailId = c.Int(nullable: false),
                        Years = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HrCvDetails", t => t.HrCvDetailId, cascadeDelete: true)
                .Index(t => t.HrCvDetailId);
            
            CreateTable(
                "dbo.HrCvMemberships",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HrCvDetailId = c.Int(nullable: false),
                        Membership = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HrCvDetails", t => t.HrCvDetailId, cascadeDelete: true)
                .Index(t => t.HrCvDetailId);
            
            CreateTable(
                "dbo.HrCvRelatives",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HrCvDetailId = c.Int(nullable: false),
                        Degree = c.String(),
                        FullName = c.String(),
                        BirthDateAndPlace = c.String(),
                        LaborDetails = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HrCvDetails", t => t.HrCvDetailId, cascadeDelete: true)
                .Index(t => t.HrCvDetailId);
            
            CreateTable(
                "dbo.HrCvHintTexts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Language = c.Int(nullable: false),
                        Field = c.Int(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);

        }
        
        public override void Down()
        {
            
            DropForeignKey("dbo.HrCvAwards", "HrCvDetailId", "dbo.HrCvDetails");
            DropForeignKey("dbo.HrCvRelatives", "HrCvDetailId", "dbo.HrCvDetails");
            DropForeignKey("dbo.HrCvMemberships", "HrCvDetailId", "dbo.HrCvDetails");
            DropForeignKey("dbo.HrCvLabors", "HrCvDetailId", "dbo.HrCvDetails");
            DropForeignKey("dbo.HrCvDetails", "EmployeeId", "dbo.HrEmployees");
            DropForeignKey("dbo.HrEmployees", "PositionId", "dbo.HrPositions");
            DropForeignKey("dbo.HrEmployeeDocuments", "EmployeeId", "dbo.HrEmployees");
            DropForeignKey("dbo.HrEmployees", "DepartmentId", "dbo.HrDepartments");
            DropForeignKey("dbo.HrCvEductions", "HrCvDetailId", "dbo.HrCvDetails");
            DropIndex("dbo.HrCvRelatives", new[] { "HrCvDetailId" });
            DropIndex("dbo.HrCvMemberships", new[] { "HrCvDetailId" });
            DropIndex("dbo.HrCvLabors", new[] { "HrCvDetailId" });
            DropIndex("dbo.HrEmployeeDocuments", new[] { "EmployeeId" });
            DropIndex("dbo.HrEmployees", new[] { "DepartmentId" });
            DropIndex("dbo.HrEmployees", new[] { "PositionId" });
            DropIndex("dbo.HrCvEductions", new[] { "HrCvDetailId" });
            DropIndex("dbo.HrCvDetails", new[] { "EmployeeId" });
            DropIndex("dbo.HrCvAwards", new[] { "HrCvDetailId" });
            DropTable("dbo.HrCvHintTexts");
            DropTable("dbo.HrCvRelatives");
            DropTable("dbo.HrCvMemberships");
            DropTable("dbo.HrCvLabors");
            DropTable("dbo.HrPositions");
            DropTable("dbo.HrEmployeeDocuments");
            DropTable("dbo.HrDepartments");
            DropTable("dbo.HrEmployees");
            DropTable("dbo.HrCvEductions");
            DropTable("dbo.HrCvDetails");
            DropTable("dbo.HrCvAwards");
        }
    }
}
