namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_TrainingSheetExercise_Charge : DbMigration
    {
        public override void Up()
        {
            AlterColumn("trainingsheetexercise", "Charge", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("trainingsheetexercise", "Charge", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
