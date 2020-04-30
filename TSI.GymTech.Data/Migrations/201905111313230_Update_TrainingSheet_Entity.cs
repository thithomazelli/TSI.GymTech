namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_TrainingSheet_Entity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.trainingsheet", "Model", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.trainingsheet", "Model");
        }
    }
}
