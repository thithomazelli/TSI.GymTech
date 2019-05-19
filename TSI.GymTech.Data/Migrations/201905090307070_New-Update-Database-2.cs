namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewUpdateDatabase2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("anamnesissheet", "Name", c => c.String(nullable: false, unicode: false));
            AddColumn("anamnesissheet", "Description", c => c.String(nullable: false, unicode: false));
            AddColumn("evaluationsheet", "Name", c => c.String(nullable: false, unicode: false));
            AddColumn("evaluationsheet", "Description", c => c.String(nullable: false, unicode: false));
            AddColumn("trainingsheetperson", "Name", c => c.String(nullable: false, unicode: false));
            AddColumn("trainingsheetperson", "Description", c => c.String(nullable: false, unicode: false));
            AddColumn("trainingsheet", "Name", c => c.String(nullable: false, unicode: false));
            AddColumn("trainingsheet", "Description", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("trainingsheet", "Description");
            DropColumn("trainingsheet", "Name");
            DropColumn("trainingsheetperson", "Description");
            DropColumn("trainingsheetperson", "Name");
            DropColumn("evaluationsheet", "Description");
            DropColumn("evaluationsheet", "Name");
            DropColumn("anamnesissheet", "Description");
            DropColumn("anamnesissheet", "Name");
        }
    }
}
