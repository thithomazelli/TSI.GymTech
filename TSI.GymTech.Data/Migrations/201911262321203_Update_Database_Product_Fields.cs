namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Database_Product_Fields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Quota", c => c.Int());
            DropColumn("dbo.Product", "Duplication");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "Duplication", c => c.Int());
            DropColumn("dbo.Product", "Quota");
        }
    }
}
