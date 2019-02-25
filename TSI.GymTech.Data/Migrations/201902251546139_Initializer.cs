namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initializer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccessControl",
                c => new
                    {
                        AccessControlId = c.Int(nullable: false, identity: true),
                        IpAddress = c.String(nullable: false, maxLength: 16, storeType: "nvarchar"),
                        Name = c.String(maxLength: 64, storeType: "nvarchar"),
                        IsStandard = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(precision: 0),
                        CreateUserId = c.Int(nullable: false),
                        ModifyDate = c.DateTime(precision: 0),
                        ModifyUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccessControlId);
            
            CreateTable(
                "dbo.AccessLog",
                c => new
                    {
                        AccessLogId = c.Int(nullable: false, identity: true),
                        AccessType = c.Int(nullable: false),
                        MessageDisplayed = c.String(unicode: false),
                        PersonId = c.Int(nullable: false),
                        AccessControlId = c.Int(nullable: false),
                        CreateDate = c.DateTime(precision: 0),
                        CreateUserId = c.Int(nullable: false),
                        ModifyDate = c.DateTime(precision: 0),
                        ModifyUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccessLogId)
                .ForeignKey("dbo.AccessControl", t => t.AccessControlId, cascadeDelete: true)
                .ForeignKey("dbo.Person", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId)
                .Index(t => t.AccessControlId);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ProfileType = c.Int(nullable: false),
                        Password = c.String(maxLength: 16, storeType: "nvarchar"),
                        Gender = c.Int(),
                        NationalIDCard = c.String(maxLength: 16, storeType: "nvarchar"),
                        SocialSecurityCard = c.String(nullable: false, maxLength: 16, storeType: "nvarchar"),
                        BirthDate = c.DateTime(precision: 0),
                        RegistrationDate = c.DateTime(precision: 0),
                        DueDate = c.DateTime(precision: 0),
                        Status = c.Int(nullable: false),
                        Photo = c.String(maxLength: 64, storeType: "nvarchar"),
                        Comments = c.String(maxLength: 1024, storeType: "nvarchar"),
                        Phone = c.String(maxLength: 32, storeType: "nvarchar"),
                        MobilePhone = c.String(maxLength: 32, storeType: "nvarchar"),
                        Email = c.String(maxLength: 64, storeType: "nvarchar"),
                        CreateDate = c.DateTime(precision: 0),
                        CreateUserId = c.Int(nullable: false),
                        ModifyDate = c.DateTime(precision: 0),
                        ModifyUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PersonId);
            
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        AddressType = c.Int(),
                        PostalCode = c.String(maxLength: 16, storeType: "nvarchar"),
                        Street = c.String(maxLength: 64, storeType: "nvarchar"),
                        Number = c.Int(),
                        Comments = c.String(maxLength: 128, storeType: "nvarchar"),
                        District = c.String(maxLength: 32, storeType: "nvarchar"),
                        State = c.String(maxLength: 32, storeType: "nvarchar"),
                        City = c.String(maxLength: 64, storeType: "nvarchar"),
                        Country = c.String(maxLength: 32, storeType: "nvarchar"),
                        PersonId = c.Int(nullable: false),
                        CreateDate = c.DateTime(precision: 0),
                        CreateUserId = c.Int(nullable: false),
                        ModifyDate = c.DateTime(precision: 0),
                        ModifyUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.Person", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.AnamnesisSheet",
                c => new
                    {
                        AnamnesisSheetId = c.Int(nullable: false, identity: true),
                        Revaluation = c.DateTime(precision: 0),
                        Comments = c.String(maxLength: 1024, storeType: "nvarchar"),
                        StudentId = c.Int(nullable: false),
                        TrainerId = c.Int(nullable: false),
                        CreateDate = c.DateTime(precision: 0),
                        CreateUserId = c.Int(nullable: false),
                        ModifyDate = c.DateTime(precision: 0),
                        ModifyUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnamnesisSheetId)
                .ForeignKey("dbo.Person", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("dbo.Person", t => t.TrainerId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.TrainerId);
            
            CreateTable(
                "dbo.EvaluationSheet",
                c => new
                    {
                        EvaluationSheetId = c.Int(nullable: false, identity: true),
                        Revaluation = c.DateTime(precision: 0),
                        Comments = c.String(maxLength: 1024, storeType: "nvarchar"),
                        StudentId = c.Int(nullable: false),
                        TrainerId = c.Int(nullable: false),
                        CreateDate = c.DateTime(precision: 0),
                        CreateUserId = c.Int(nullable: false),
                        ModifyDate = c.DateTime(precision: 0),
                        ModifyUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EvaluationSheetId)
                .ForeignKey("dbo.Person", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("dbo.Person", t => t.TrainerId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.TrainerId);
            
            CreateTable(
                "dbo.Exercise",
                c => new
                    {
                        ExerciseId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Description = c.String(maxLength: 128, storeType: "nvarchar"),
                        Photo = c.String(maxLength: 64, storeType: "nvarchar"),
                        Comments = c.String(maxLength: 1024, storeType: "nvarchar"),
                        MuscleWorked = c.String(maxLength: 64, storeType: "nvarchar"),
                        MuscularGroup = c.Int(),
                        CreateDate = c.DateTime(precision: 0),
                        CreateUserId = c.Int(nullable: false),
                        ModifyDate = c.DateTime(precision: 0),
                        ModifyUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExerciseId);
            
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(maxLength: 128, storeType: "nvarchar"),
                        PersonId = c.Int(nullable: false),
                        Discount = c.Decimal(precision: 18, scale: 2),
                        TotalPrice = c.Decimal(precision: 18, scale: 2),
                        PaymentType = c.Int(),
                        Status = c.Int(nullable: false),
                        DatePaymentEstimated = c.DateTime(precision: 0),
                        DatePaymentCompleted = c.DateTime(precision: 0),
                        Comments = c.String(maxLength: 1024, storeType: "nvarchar"),
                        CreateDate = c.DateTime(precision: 0),
                        CreateUserId = c.Int(nullable: false),
                        ModifyDate = c.DateTime(precision: 0),
                        ModifyUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("dbo.Person", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.PaymentProduct",
                c => new
                    {
                        PaymentProductId = c.Int(nullable: false, identity: true),
                        PaymentId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(),
                        Discount = c.Decimal(precision: 18, scale: 2),
                        UnitPrice = c.Decimal(precision: 18, scale: 2),
                        TotalPrice = c.Decimal(precision: 18, scale: 2),
                        CreateDate = c.DateTime(precision: 0),
                        CreateUserId = c.Int(nullable: false),
                        ModifyDate = c.DateTime(precision: 0),
                        ModifyUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentProductId)
                .ForeignKey("dbo.Payment", t => t.PaymentId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.PaymentId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(maxLength: 128, storeType: "nvarchar"),
                        Description = c.String(maxLength: 128, storeType: "nvarchar"),
                        ProductType = c.Int(nullable: false),
                        SuggestedPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QuantityStock = c.Int(),
                        Duplication = c.Int(),
                        Photo = c.String(maxLength: 64, storeType: "nvarchar"),
                        Comments = c.String(maxLength: 1024, storeType: "nvarchar"),
                        CreateDate = c.DateTime(precision: 0),
                        CreateUserId = c.Int(nullable: false),
                        ModifyDate = c.DateTime(precision: 0),
                        ModifyUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.SheetAnswer",
                c => new
                    {
                        SheetAnswerId = c.Int(nullable: false, identity: true),
                        Answer = c.String(maxLength: 128, storeType: "nvarchar"),
                        SheetId = c.Int(nullable: false),
                        SheetQuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SheetAnswerId)
                .ForeignKey("dbo.AnamnesisSheet", t => t.SheetId, cascadeDelete: true)
                .ForeignKey("dbo.EvaluationSheet", t => t.SheetId, cascadeDelete: true)
                .ForeignKey("dbo.SheetQuestion", t => t.SheetQuestionId, cascadeDelete: true)
                .Index(t => t.SheetId)
                .Index(t => t.SheetQuestionId);
            
            CreateTable(
                "dbo.SheetQuestion",
                c => new
                    {
                        SheetQuestionId = c.Int(nullable: false, identity: true),
                        TypeQuestion = c.Int(nullable: false),
                        Order = c.Int(),
                        Question = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.SheetQuestionId);
            
            CreateTable(
                "dbo.TrainingSheet",
                c => new
                    {
                        TrainingSheetId = c.Int(nullable: false, identity: true),
                        Cycle = c.String(maxLength: 64, storeType: "nvarchar"),
                        Status = c.Int(),
                        Type = c.Int(nullable: false),
                        Revaluation = c.DateTime(precision: 0),
                        Comments = c.String(maxLength: 1024, storeType: "nvarchar"),
                        StudentId = c.Int(nullable: false),
                        TrainerId = c.Int(nullable: false),
                        CreateDate = c.DateTime(precision: 0),
                        CreateUserId = c.Int(nullable: false),
                        ModifyDate = c.DateTime(precision: 0),
                        ModifyUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TrainingSheetId)
                .ForeignKey("dbo.Person", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("dbo.Person", t => t.TrainerId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.TrainerId);
            
            CreateTable(
                "dbo.TrainingSheetExercise",
                c => new
                    {
                        TrainingSheetExerciseId = c.Int(nullable: false, identity: true),
                        TrainingSheetId = c.Int(nullable: false),
                        ExerciseId = c.Int(nullable: false),
                        Serie = c.Int(nullable: false),
                        NumberOfRepetitions = c.Int(nullable: false),
                        Charge = c.Decimal(precision: 18, scale: 2),
                        CreateDate = c.DateTime(precision: 0),
                        CreateUserId = c.Int(nullable: false),
                        ModifyDate = c.DateTime(precision: 0),
                        ModifyUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TrainingSheetExerciseId)
                .ForeignKey("dbo.Exercise", t => t.ExerciseId, cascadeDelete: true)
                .ForeignKey("dbo.TrainingSheet", t => t.TrainingSheetId, cascadeDelete: true)
                .Index(t => t.TrainingSheetId)
                .Index(t => t.ExerciseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrainingSheetExercise", "TrainingSheetId", "dbo.TrainingSheet");
            DropForeignKey("dbo.TrainingSheetExercise", "ExerciseId", "dbo.Exercise");
            DropForeignKey("dbo.TrainingSheet", "TrainerId", "dbo.Person");
            DropForeignKey("dbo.TrainingSheet", "StudentId", "dbo.Person");
            DropForeignKey("dbo.SheetAnswer", "SheetQuestionId", "dbo.SheetQuestion");
            DropForeignKey("dbo.SheetAnswer", "SheetId", "dbo.EvaluationSheet");
            DropForeignKey("dbo.SheetAnswer", "SheetId", "dbo.AnamnesisSheet");
            DropForeignKey("dbo.PaymentProduct", "ProductId", "dbo.Product");
            DropForeignKey("dbo.PaymentProduct", "PaymentId", "dbo.Payment");
            DropForeignKey("dbo.Payment", "PersonId", "dbo.Person");
            DropForeignKey("dbo.EvaluationSheet", "TrainerId", "dbo.Person");
            DropForeignKey("dbo.EvaluationSheet", "StudentId", "dbo.Person");
            DropForeignKey("dbo.AnamnesisSheet", "TrainerId", "dbo.Person");
            DropForeignKey("dbo.AnamnesisSheet", "StudentId", "dbo.Person");
            DropForeignKey("dbo.Address", "PersonId", "dbo.Person");
            DropForeignKey("dbo.AccessLog", "PersonId", "dbo.Person");
            DropForeignKey("dbo.AccessLog", "AccessControlId", "dbo.AccessControl");
            DropIndex("dbo.TrainingSheetExercise", new[] { "ExerciseId" });
            DropIndex("dbo.TrainingSheetExercise", new[] { "TrainingSheetId" });
            DropIndex("dbo.TrainingSheet", new[] { "TrainerId" });
            DropIndex("dbo.TrainingSheet", new[] { "StudentId" });
            DropIndex("dbo.SheetAnswer", new[] { "SheetQuestionId" });
            DropIndex("dbo.SheetAnswer", new[] { "SheetId" });
            DropIndex("dbo.PaymentProduct", new[] { "ProductId" });
            DropIndex("dbo.PaymentProduct", new[] { "PaymentId" });
            DropIndex("dbo.Payment", new[] { "PersonId" });
            DropIndex("dbo.EvaluationSheet", new[] { "TrainerId" });
            DropIndex("dbo.EvaluationSheet", new[] { "StudentId" });
            DropIndex("dbo.AnamnesisSheet", new[] { "TrainerId" });
            DropIndex("dbo.AnamnesisSheet", new[] { "StudentId" });
            DropIndex("dbo.Address", new[] { "PersonId" });
            DropIndex("dbo.AccessLog", new[] { "AccessControlId" });
            DropIndex("dbo.AccessLog", new[] { "PersonId" });
            DropTable("dbo.TrainingSheetExercise");
            DropTable("dbo.TrainingSheet");
            DropTable("dbo.SheetQuestion");
            DropTable("dbo.SheetAnswer");
            DropTable("dbo.Product");
            DropTable("dbo.PaymentProduct");
            DropTable("dbo.Payment");
            DropTable("dbo.Exercise");
            DropTable("dbo.EvaluationSheet");
            DropTable("dbo.AnamnesisSheet");
            DropTable("dbo.Address");
            DropTable("dbo.Person");
            DropTable("dbo.AccessLog");
            DropTable("dbo.AccessControl");
        }
    }
}
