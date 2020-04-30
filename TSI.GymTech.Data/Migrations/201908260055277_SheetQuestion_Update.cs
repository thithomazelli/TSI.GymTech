namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SheetQuestion_Update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SheetQuestion", "AnswerType", c => c.Int(nullable: false));
            AddColumn("dbo.SheetQuestion", "QuestionType", c => c.Int(nullable: false));
            DropColumn("dbo.SheetQuestion", "TypeQuestion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SheetQuestion", "TypeQuestion", c => c.Int(nullable: false));
            DropColumn("dbo.SheetQuestion", "QuestionType");
            DropColumn("dbo.SheetQuestion", "AnswerType");
        }
    }
}
