namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_TrainingSheet_Person_Relashionships : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("anamnesissheet", "StudentId", "person");
            DropForeignKey("evaluationsheet", "StudentId", "person");
            DropIndex("anamnesissheet", new[] { "StudentId" });
            DropIndex("evaluationsheet", new[] { "StudentId" });
            AlterColumn("anamnesissheet", "StudentId", c => c.Int(nullable: false));
            AlterColumn("evaluationsheet", "StudentId", c => c.Int(nullable: false));
            CreateIndex("anamnesissheet", "StudentId");
            CreateIndex("evaluationsheet", "StudentId");
            AddForeignKey("anamnesissheet", "StudentId", "person", "PersonId", cascadeDelete: true);
            AddForeignKey("evaluationsheet", "StudentId", "person", "PersonId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("evaluationsheet", "StudentId", "person");
            DropForeignKey("anamnesissheet", "StudentId", "person");
            DropIndex("evaluationsheet", new[] { "StudentId" });
            DropIndex("anamnesissheet", new[] { "StudentId" });
            AlterColumn("evaluationsheet", "StudentId", c => c.Int());
            AlterColumn("anamnesissheet", "StudentId", c => c.Int());
            CreateIndex("evaluationsheet", "StudentId");
            CreateIndex("anamnesissheet", "StudentId");
            AddForeignKey("evaluationsheet", "StudentId", "person", "PersonId");
            AddForeignKey("anamnesissheet", "StudentId", "person", "PersonId");
        }
    }
}
