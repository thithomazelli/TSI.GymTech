namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reinitialize_Product : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Product", "ProductStatus");
            DropColumn("dbo.Product", "SuggestedPrice");
            DropColumn("dbo.Product", "QuantityStock");
            DropColumn("dbo.Product", "Duplication");
            DropColumn("dbo.Product", "Photo");
            DropColumn("dbo.Product", "Comments");
            DropColumn("dbo.Product", "CreateDate");
            DropColumn("dbo.Product", "CreateUserId");
            DropColumn("dbo.Product", "ModifyDate");
            DropColumn("dbo.Product", "ModifyUserId");

            AddColumn("dbo.Product", "ProductStatus", c => c.Int());
            AddColumn("dbo.Product", "SuggestedPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Product", "QuantityStock", c => c.Int());
            AddColumn("dbo.Product", "Duplication", c => c.Int());
            AddColumn("dbo.Product", "Photo", c => c.String(maxLength: 64, storeType: "nvarchar"));
            AddColumn("dbo.Product", "Comments", c => c.String(maxLength: 1024, storeType: "nvarchar"));
            AddColumn("dbo.Product", "CreateDate", c => c.DateTime(precision: 0));
            AddColumn("dbo.Product", "CreateUserId", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "ModifyDate", c => c.DateTime(precision: 0));
            AddColumn("dbo.Product", "ModifyUserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
        }
    }
}
