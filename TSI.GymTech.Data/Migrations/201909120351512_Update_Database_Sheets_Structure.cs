namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Database_Sheets_Structure : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("SheetAnswer", "SheetId", "AnamnesisSheet");
            DropForeignKey("SheetAnswer", "SheetId", "EvaluationSheet");
            DropForeignKey("SheetAnswer", "SheetQuestionId", "SheetQuestion");
            DropIndex("SheetAnswer", new[] { "SheetId" });
            DropIndex("SheetAnswer", new[] { "SheetQuestionId" });
            CreateTable(
                "AnamnesisSheetAnswer",
                c => new
                    {
                        AnamnesisSheetAnswerId = c.Int(nullable: false, identity: true),
                        AnamnesisSheetId = c.Int(nullable: false),
                        Answer = c.String(maxLength: 128, storeType: "nvarchar"),
                        SheetQuestionId = c.Int(nullable: false),
                        CreateDate = c.DateTime(precision: 0),
                        CreateUserId = c.Int(nullable: false),
                        ModifyDate = c.DateTime(precision: 0),
                        ModifyUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnamnesisSheetAnswerId)
                .ForeignKey("AnamnesisSheet", t => t.AnamnesisSheetId, cascadeDelete: true)
                .ForeignKey("SheetQuestion", t => t.SheetQuestionId, cascadeDelete: true)
                .Index(t => t.AnamnesisSheetId)
                .Index(t => t.SheetQuestionId);
            
            CreateTable(
                "EvaluationSheetAnswer",
                c => new
                    {
                        EvaluationSheetAnswerId = c.Int(nullable: false, identity: true),
                        EvaluationSheetId = c.Int(nullable: false),
                        Answer = c.String(maxLength: 128, storeType: "nvarchar"),
                        SheetQuestionId = c.Int(nullable: false),
                        CreateDate = c.DateTime(precision: 0),
                        CreateUserId = c.Int(nullable: false),
                        ModifyDate = c.DateTime(precision: 0),
                        ModifyUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EvaluationSheetAnswerId)
                .ForeignKey("EvaluationSheet", t => t.EvaluationSheetId, cascadeDelete: true)
                .ForeignKey("SheetQuestion", t => t.SheetQuestionId, cascadeDelete: true)
                .Index(t => t.EvaluationSheetId)
                .Index(t => t.SheetQuestionId);
            
            AddColumn("SheetQuestion", "AnswerType", c => c.Int(nullable: false));
            AddColumn("SheetQuestion", "QuestionType", c => c.Int(nullable: false));
            DropColumn("AnamnesisSheet", "Name");
            DropColumn("EvaluationSheet", "Name");
            DropColumn("SheetQuestion", "TypeQuestion");
            DropTable("SheetAnswer");
        }
        
        public override void Down()
        {
            CreateTable(
                "SheetAnswer",
                c => new
                    {
                        SheetAnswerId = c.Int(nullable: false, identity: true),
                        Answer = c.String(maxLength: 128, storeType: "nvarchar"),
                        SheetId = c.Int(nullable: false),
                        SheetQuestionId = c.Int(nullable: false),
                        CreateDate = c.DateTime(precision: 0),
                        CreateUserId = c.Int(nullable: false),
                        ModifyDate = c.DateTime(precision: 0),
                        ModifyUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SheetAnswerId);
            
            AddColumn("SheetQuestion", "TypeQuestion", c => c.Int(nullable: false));
            AddColumn("EvaluationSheet", "Name", c => c.String(nullable: false, unicode: false));
            AddColumn("AnamnesisSheet", "Name", c => c.String(nullable: false, unicode: false));
            DropForeignKey("EvaluationSheetAnswer", "SheetQuestionId", "SheetQuestion");
            DropForeignKey("EvaluationSheetAnswer", "EvaluationSheetId", "EvaluationSheet");
            DropForeignKey("AnamnesisSheetAnswer", "SheetQuestionId", "SheetQuestion");
            DropForeignKey("AnamnesisSheetAnswer", "AnamnesisSheetId", "AnamnesisSheet");
            DropIndex("EvaluationSheetAnswer", new[] { "SheetQuestionId" });
            DropIndex("EvaluationSheetAnswer", new[] { "EvaluationSheetId" });
            DropIndex("AnamnesisSheetAnswer", new[] { "SheetQuestionId" });
            DropIndex("AnamnesisSheetAnswer", new[] { "AnamnesisSheetId" });
            DropColumn("SheetQuestion", "QuestionType");
            DropColumn("SheetQuestion", "AnswerType");
            DropTable("EvaluationSheetAnswer");
            DropTable("AnamnesisSheetAnswer");
            CreateIndex("SheetAnswer", "SheetQuestionId");
            CreateIndex("SheetAnswer", "SheetId");
            AddForeignKey("SheetAnswer", "SheetQuestionId", "SheetQuestion", "SheetQuestionId", cascadeDelete: true);
            AddForeignKey("SheetAnswer", "SheetId", "EvaluationSheet", "EvaluationSheetId", cascadeDelete: true);
            AddForeignKey("SheetAnswer", "SheetId", "AnamnesisSheet", "AnamnesisSheetId", cascadeDelete: true);
        }
    }
}
