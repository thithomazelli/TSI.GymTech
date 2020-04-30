namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_TrainingSheetExercise_Entity : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.trainingsheetexercise", "TrainingSheetId", "dbo.trainingsheet", "TrainingSheetId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.trainingsheetexercise", "TrainingSheetId", "dbo.trainingsheet");
        }
    }
}
