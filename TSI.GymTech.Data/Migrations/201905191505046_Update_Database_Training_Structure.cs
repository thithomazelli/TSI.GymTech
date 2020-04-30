namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Database_Training_Structure : DbMigration
    {
        public override void Up()
        {
            DropIndex("AnamnesisSheet", new[] { "TrainerId" });
            DropIndex("EvaluationSheet", new[] { "TrainerId" });
            DropIndex("TrainingSheet", new[] { "StudentId" });
            DropIndex("TrainingSheet", new[] { "TrainerId" });
            AddColumn("AnamnesisSheet", "Name", c => c.String(nullable: false, unicode: false));
            AddColumn("AnamnesisSheet", "Description", c => c.String(nullable: false, unicode: false));
            AddColumn("AnamnesisSheet", "Cycle", c => c.String(maxLength: 64, storeType: "nvarchar"));
            AddColumn("AnamnesisSheet", "Status", c => c.Int());
            AddColumn("EvaluationSheet", "Name", c => c.String(nullable: false, unicode: false));
            AddColumn("EvaluationSheet", "Description", c => c.String(nullable: false, unicode: false));
            AddColumn("EvaluationSheet", "Cycle", c => c.String(maxLength: 64, storeType: "nvarchar"));
            AddColumn("EvaluationSheet", "Status", c => c.Int());
            AddColumn("TrainingSheet", "Model", c => c.Int(nullable: false));
            AddColumn("TrainingSheet", "Name", c => c.String(nullable: false, unicode: false));
            AddColumn("TrainingSheet", "Description", c => c.String(nullable: false, unicode: false));
            AlterColumn("TrainingSheet", "StudentId", c => c.Int());
            AlterColumn("TrainingSheetExercise", "Charge", c => c.String(unicode: false));
            CreateIndex("TrainingSheet", "StudentId");
            AddForeignKey("TrainingSheet", "StudentId", "Person", "PersonId");
            DropColumn("AnamnesisSheet", "TrainerId");
            DropColumn("EvaluationSheet", "TrainerId");
            DropColumn("TrainingSheet", "TrainerId");
        }
        
        public override void Down()
        {
            AddColumn("TrainingSheet", "TrainerId", c => c.Int(nullable: false));
            AddColumn("EvaluationSheet", "TrainerId", c => c.Int(nullable: false));
            AddColumn("AnamnesisSheet", "TrainerId", c => c.Int(nullable: false));
            DropForeignKey("TrainingSheet", "StudentId", "Person");
            DropIndex("TrainingSheet", new[] { "StudentId" });
            AlterColumn("TrainingSheetExercise", "Charge", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("TrainingSheet", "StudentId", c => c.Int(nullable: false));
            DropColumn("TrainingSheet", "Description");
            DropColumn("TrainingSheet", "Name");
            DropColumn("TrainingSheet", "Model");
            DropColumn("EvaluationSheet", "Status");
            DropColumn("EvaluationSheet", "Cycle");
            DropColumn("EvaluationSheet", "Description");
            DropColumn("EvaluationSheet", "Name");
            DropColumn("AnamnesisSheet", "Status");
            DropColumn("AnamnesisSheet", "Cycle");
            DropColumn("AnamnesisSheet", "Description");
            DropColumn("AnamnesisSheet", "Name");
            CreateIndex("TrainingSheet", "TrainerId");
            CreateIndex("TrainingSheet", "StudentId");
            CreateIndex("EvaluationSheet", "TrainerId");
            CreateIndex("AnamnesisSheet", "TrainerId");
            AddForeignKey("TrainingSheet", "StudentId", "Person", "PersonId", cascadeDelete: true);
            AddForeignKey("TrainingSheet", "TrainerId", "Person", "PersonId", cascadeDelete: true);
            AddForeignKey("EvaluationSheet", "TrainerId", "Person", "PersonId", cascadeDelete: true);
            AddForeignKey("AnamnesisSheet", "TrainerId", "Person", "PersonId", cascadeDelete: true);
        }
    }
}
