namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_TrainingSheetExercise : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TrainingSheetExercise", "Serie", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.TrainingSheetExercise", "NumberOfRepetitions", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TrainingSheetExercise", "NumberOfRepetitions", c => c.Int(nullable: false));
            AlterColumn("dbo.TrainingSheetExercise", "Serie", c => c.Int(nullable: false));
        }
    }
}
