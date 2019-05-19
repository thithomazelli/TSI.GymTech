namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_TrainingSheet_Person_Relashionship_Nullable_NotRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("anamnesissheet", "StudentId", "person");
            DropForeignKey("evaluationsheet", "StudentId", "person");
            DropForeignKey("trainingsheet", "StudentId", "person");
            DropIndex("anamnesissheet", new[] { "StudentId" });
            DropIndex("evaluationsheet", new[] { "StudentId" });
            DropIndex("trainingsheet", new[] { "StudentId" });
            AlterColumn("anamnesissheet", "StudentId", c => c.Int());
            AlterColumn("evaluationsheet", "StudentId", c => c.Int());
            AlterColumn("trainingsheet", "StudentId", c => c.Int());
            CreateIndex("anamnesissheet", "StudentId");
            CreateIndex("evaluationsheet", "StudentId");
            CreateIndex("trainingsheet", "StudentId");
            AddForeignKey("anamnesissheet", "StudentId", "person", "PersonId");
            AddForeignKey("evaluationsheet", "StudentId", "person", "PersonId");
            AddForeignKey("trainingsheet", "StudentId", "person", "PersonId");
        }
        
        public override void Down()
        {
            DropForeignKey("trainingsheet", "StudentId", "person");
            DropForeignKey("evaluationsheet", "StudentId", "person");
            DropForeignKey("anamnesissheet", "StudentId", "person");
            DropIndex("trainingsheet", new[] { "StudentId" });
            DropIndex("evaluationsheet", new[] { "StudentId" });
            DropIndex("anamnesissheet", new[] { "StudentId" });
            AlterColumn("trainingsheet", "StudentId", c => c.Int(nullable: false));
            AlterColumn("evaluationsheet", "StudentId", c => c.Int(nullable: false));
            AlterColumn("anamnesissheet", "StudentId", c => c.Int(nullable: false));
            CreateIndex("trainingsheet", "StudentId");
            CreateIndex("evaluationsheet", "StudentId");
            CreateIndex("anamnesissheet", "StudentId");
            AddForeignKey("trainingsheet", "StudentId", "person", "PersonId", cascadeDelete: true);
            AddForeignKey("evaluationsheet", "StudentId", "person", "PersonId", cascadeDelete: true);
            AddForeignKey("anamnesissheet", "StudentId", "person", "PersonId", cascadeDelete: true);
        }
    }
}
