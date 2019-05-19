namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_TrainingSheet_Person_Relashionship : DbMigration
    {
        public override void Up()
        {
            AddColumn("trainingsheet", "StudentId", c => c.Int(nullable: false));
            CreateIndex("trainingsheet", "StudentId");
            AddForeignKey("trainingsheet", "StudentId", "person", "PersonId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("trainingsheet", "StudentId", "person");
            DropIndex("trainingsheet", new[] { "StudentId" });
            DropColumn("trainingsheet", "StudentId");
        }
    }
}
