namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_TrainingSheetPerson_Entity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("trainingsheetexercise", "TrainingSheetId", "trainingsheetperson");
        }
        
        public override void Down()
        {
            AddForeignKey("trainingsheetexercise", "TrainingSheetId", "trainingsheetperson", "TrainingSheetPersonId", cascadeDelete: true);
        }
    }
}
