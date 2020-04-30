namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewUpdateDatabase : DbMigration
    {
        public override void Up()
        {
            DropColumn("trainingsheetperson", "PersonId");
            AddColumn("anamnesissheet", "Cycle", c => c.String(maxLength: 64, storeType: "nvarchar"));
            AddColumn("anamnesissheet", "Status", c => c.Int());
            AddColumn("evaluationsheet", "Cycle", c => c.String(maxLength: 64, storeType: "nvarchar"));
            AddColumn("evaluationsheet", "Status", c => c.Int());
            AddColumn("trainingsheet", "Revaluation", c => c.DateTime(precision: 0));
            AddColumn("trainingsheet", "Comments", c => c.String(maxLength: 1024, storeType: "nvarchar"));
            DropColumn("anamnesissheet", "TrainerId");
            DropColumn("evaluationsheet", "TrainerId");
            DropColumn("trainingsheetperson", "TrainerId");
        }
        
        public override void Down()
        {
            AddColumn("trainingsheetperson", "TrainerId", c => c.Int(nullable: false));
            AddColumn("evaluationsheet", "TrainerId", c => c.Int(nullable: false));
            AddColumn("anamnesissheet", "TrainerId", c => c.Int(nullable: false));
            DropColumn("trainingsheet", "Comments");
            DropColumn("trainingsheet", "Revaluation");
            DropColumn("evaluationsheet", "Status");
            DropColumn("evaluationsheet", "Cycle");
            DropColumn("anamnesissheet", "Status");
            DropColumn("anamnesissheet", "Cycle");
            RenameColumn(table: "trainingsheetperson", name: "StudentId", newName: "PersonId");
            AddColumn("trainingsheetperson", "StudentId", c => c.Int(nullable: false));

        }
    }
}
