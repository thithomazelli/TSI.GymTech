namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTraining_Structure : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("trainingsheetexercise", "TrainingSheetId", "trainingsheet");
            DropForeignKey("trainingsheet", "StudentId", "person");
            DropIndex("trainingsheet", new[] { "StudentId" });
            CreateTable(
                "trainingsheetperson",
                c => new
                    {
                        TrainingSheetPersonId = c.Int(nullable: false, identity: true),
                        Cycle = c.String(maxLength: 64, storeType: "nvarchar"),
                        Status = c.Int(),
                        Type = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                        Revaluation = c.DateTime(precision: 0),
                        Comments = c.String(maxLength: 1024, storeType: "nvarchar"),
                        StudentId = c.Int(nullable: false),
                        TrainerId = c.Int(nullable: false),
                        CreateDate = c.DateTime(precision: 0),
                        CreateUserId = c.Int(nullable: false),
                        ModifyDate = c.DateTime(precision: 0),
                        ModifyUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TrainingSheetPersonId)
                .ForeignKey("person", t => t.PersonId, cascadeDelete: true)
                .ForeignKey("person", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.PersonId)
                .Index(t => t.StudentId);
            
            AddForeignKey("trainingsheetexercise", "TrainingSheetId", "trainingsheetperson", "TrainingSheetPersonId", cascadeDelete: true);
            DropColumn("trainingsheet", "Revaluation");
            DropColumn("trainingsheet", "Comments");
            DropColumn("trainingsheet", "StudentId");
            DropColumn("trainingsheet", "TrainerId");
        }
        
        public override void Down()
        {
            AddColumn("trainingsheet", "TrainerId", c => c.Int(nullable: false));
            AddColumn("trainingsheet", "StudentId", c => c.Int(nullable: false));
            AddColumn("trainingsheet", "Comments", c => c.String(maxLength: 1024, storeType: "nvarchar"));
            AddColumn("trainingsheet", "Revaluation", c => c.DateTime(precision: 0));
            DropForeignKey("trainingsheetexercise", "TrainingSheetId", "trainingsheetperson");
            DropForeignKey("trainingsheetperson", "StudentId", "person");
            DropForeignKey("trainingsheetperson", "PersonId", "person");
            DropIndex("trainingsheetperson", new[] { "StudentId" });
            DropIndex("trainingsheetperson", new[] { "PersonId" });
            DropTable("trainingsheetperson");
            CreateIndex("trainingsheet", "StudentId");
            AddForeignKey("trainingsheet", "StudentId", "person", "PersonId", cascadeDelete: true);
            AddForeignKey("trainingsheetexercise", "TrainingSheetId", "trainingsheet", "TrainingSheetId", cascadeDelete: true);
        }
    }
}
