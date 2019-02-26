namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Product : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "ProductStatus", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "ProductStatus");
        }
    }
}
