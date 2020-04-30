namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Database : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("anamnesissheet", "TrainerId", "Person");
            DropForeignKey("evaluationsheet", "TrainerId", "Person");
            DropForeignKey("trainingsheet", "TrainerId", "Person");
            DropIndex("anamnesissheet", new[] { "TrainerId" });
            DropIndex("evaluationsheet", new[] { "TrainerId" });
            DropIndex("trainingsheet", new[] { "TrainerId" });
        }
        
        public override void Down()
        {
            CreateIndex("trainingsheet", "TrainerId");
            CreateIndex("evaluationsheet", "TrainerId");
            CreateIndex("anamnesissheet", "TrainerId");
            AddForeignKey("trainingsheet", "TrainerId", "Person", "PersonId", cascadeDelete: true);
            AddForeignKey("evaluationsheet", "TrainerId", "Person", "PersonId", cascadeDelete: true);
            AddForeignKey("anamnesissheet", "TrainerId", "Person", "PersonId", cascadeDelete: true);
        }
    }
}
