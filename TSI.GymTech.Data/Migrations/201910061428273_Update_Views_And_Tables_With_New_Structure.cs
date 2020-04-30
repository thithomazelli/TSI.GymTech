namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Views_And_Tables_With_New_Structure : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("AnamnesisSheetAnswer", "AnamnesisSheetId", "AnamnesisSheet");
            DropForeignKey("AnamnesisSheet", "StudentId", "Person");
            DropIndex("AnamnesisSheet", new[] { "StudentId" });
            DropIndex("AnamnesisSheetAnswer", new[] { "AnamnesisSheetId" });
            AddColumn("AnamnesisSheetAnswer", "EvaluationSheetId", c => c.Int(nullable: false));
            CreateIndex("AnamnesisSheetAnswer", "EvaluationSheetId");
            AddForeignKey("AnamnesisSheetAnswer", "EvaluationSheetId", "EvaluationSheet", "EvaluationSheetId", cascadeDelete: true);
            DropColumn("AnamnesisSheetAnswer", "AnamnesisSheetId");
            DropTable("AnamnesisSheet");
            //DropPrimaryKey("AccessLogView");
            //DropPrimaryKey("EvaluationSheetView");
            //DropPrimaryKey("TrainingSheetView");
            //AlterColumn("AccessLogView", "AccessLogId", c => c.Int(nullable: false, identity: true));
            //AlterColumn("AccessLogView", "PersonId", c => c.Int(nullable: false));
            //AlterColumn("AccessLogView", "PersonProfileType", c => c.Int(nullable: false));
            //AlterColumn("AccessLogView", "AccessType", c => c.Int(nullable: false));
            //AlterColumn("AccessLogView", "CreateDate", c => c.DateTime(nullable: false, precision: 0));
            //AlterColumn("EvaluationSheetView", "EvaluationSheetId", c => c.Int(nullable: false, identity: true));
            //AlterColumn("EvaluationSheetView", "StudentId", c => c.Int(nullable: false));
            //AlterColumn("EvaluationSheetView", "Revaluation", c => c.DateTime(precision: 0));
            //AlterColumn("EvaluationSheetView", "Status", c => c.Int());
            //AlterColumn("TrainingSheetView", "TrainingSheetId", c => c.Int(nullable: false, identity: true));
            //AlterColumn("TrainingSheetView", "StudentId", c => c.Int());
            //AlterColumn("TrainingSheetView", "Model", c => c.Int(nullable: false));
            //AlterColumn("TrainingSheetView", "Status", c => c.Int());
            //AlterColumn("TrainingSheetView", "Type", c => c.Int(nullable: false));
            //AddPrimaryKey("AccessLogView", "AccessLogId");
            //AddPrimaryKey("EvaluationSheetView", "EvaluationSheetId");
            //AddPrimaryKey("TrainingSheetView", "TrainingSheetId");
            //DropTable("AnamnesisSheetView");
        }

        public override void Down()
        {
            CreateTable(
                "AnamnesisSheetView",
                c => new
                    {
                        AnamnesisSheetId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                        StudentId = c.String(unicode: false),
                        StudentName = c.String(unicode: false),
                        Revaluation = c.String(unicode: false),
                        Status = c.String(unicode: false),
                        Comments = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.AnamnesisSheetId);
            
            CreateTable(
                "AnamnesisSheet",
                c => new
                    {
                        AnamnesisSheetId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
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
                .PrimaryKey(t => t.AnamnesisSheetId);
            
            AddColumn("AnamnesisSheetAnswer", "AnamnesisSheetId", c => c.Int(nullable: false));
            DropForeignKey("AnamnesisSheetAnswer", "EvaluationSheetId", "EvaluationSheet");
            DropIndex("AnamnesisSheetAnswer", new[] { "EvaluationSheetId" });
            DropPrimaryKey("TrainingSheetView");
            DropPrimaryKey("EvaluationSheetView");
            DropPrimaryKey("AccessLogView");
            AlterColumn("TrainingSheetView", "Type", c => c.String(unicode: false));
            AlterColumn("TrainingSheetView", "Status", c => c.String(unicode: false));
            AlterColumn("TrainingSheetView", "Model", c => c.String(unicode: false));
            AlterColumn("TrainingSheetView", "StudentId", c => c.String(unicode: false));
            AlterColumn("TrainingSheetView", "TrainingSheetId", c => c.String(nullable: false, maxLength: 128, storeType: "nvarchar"));
            AlterColumn("EvaluationSheetView", "Status", c => c.String(unicode: false));
            AlterColumn("EvaluationSheetView", "Revaluation", c => c.String(unicode: false));
            AlterColumn("EvaluationSheetView", "StudentId", c => c.String(unicode: false));
            AlterColumn("EvaluationSheetView", "EvaluationSheetId", c => c.String(nullable: false, maxLength: 128, storeType: "nvarchar"));
            AlterColumn("AccessLogView", "CreateDate", c => c.String(unicode: false));
            AlterColumn("AccessLogView", "AccessType", c => c.String(unicode: false));
            AlterColumn("AccessLogView", "PersonProfileType", c => c.String(unicode: false));
            AlterColumn("AccessLogView", "PersonId", c => c.String(unicode: false));
            AlterColumn("AccessLogView", "AccessLogId", c => c.String(nullable: false, maxLength: 128, storeType: "nvarchar"));
            DropColumn("AnamnesisSheetAnswer", "EvaluationSheetId");
            AddPrimaryKey("TrainingSheetView", "TrainingSheetId");
            AddPrimaryKey("EvaluationSheetView", "EvaluationSheetId");
            AddPrimaryKey("AccessLogView", "AccessLogId");
            CreateIndex("AnamnesisSheetAnswer", "AnamnesisSheetId");
            CreateIndex("AnamnesisSheet", "StudentId");
            AddForeignKey("AnamnesisSheet", "StudentId", "Person", "PersonId", cascadeDelete: true);
            AddForeignKey("AnamnesisSheetAnswer", "AnamnesisSheetId", "AnamnesisSheet", "AnamnesisSheetId", cascadeDelete: true);
        }
    }
}
