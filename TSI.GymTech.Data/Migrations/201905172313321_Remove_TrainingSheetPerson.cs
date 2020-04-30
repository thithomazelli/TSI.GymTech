namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_TrainingSheetPerson : DbMigration
    {
        public override void Up()
        {
            DropTable("trainingsheetperson");
        }
        
        public override void Down()
        {
            CreateTable(
                "trainingsheetperson",
                c => new
                    {
                        TrainingSheetPersonId = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        Name = c.String(nullable: false, unicode: false),
                        Description = c.String(nullable: false, unicode: false),
                        Cycle = c.String(maxLength: 64, storeType: "nvarchar"),
                        Status = c.Int(),
                        Revaluation = c.DateTime(precision: 0),
                        Comments = c.String(maxLength: 1024, storeType: "nvarchar"),
                        CreateDate = c.DateTime(precision: 0),
                        CreateUserId = c.Int(nullable: false),
                        ModifyDate = c.DateTime(precision: 0),
                        ModifyUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TrainingSheetPersonId);
            
            DropForeignKey("trainingsheet", "StudentId", "person");
            CreateIndex("trainingsheetperson", "StudentId");
            AddForeignKey("trainingsheetperson", "StudentId", "person", "PersonId", cascadeDelete: true);
        }
    }
}
