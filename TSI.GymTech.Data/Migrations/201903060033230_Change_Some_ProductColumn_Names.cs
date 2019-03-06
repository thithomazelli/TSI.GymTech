namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_Some_ProductColumn_Names : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Name", c => c.String(maxLength: 128, storeType: "nvarchar"));
            AddColumn("dbo.Product", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "Status", c => c.Int());
            DropColumn("dbo.Payment", "ProductName");
            DropColumn("dbo.Product", "ProductName");
            DropColumn("dbo.Product", "ProductType");
            DropColumn("dbo.Product", "ProductStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "ProductStatus", c => c.Int());
            AddColumn("dbo.Product", "ProductType", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "ProductName", c => c.String(maxLength: 128, storeType: "nvarchar"));
            AddColumn("dbo.Payment", "ProductName", c => c.String(maxLength: 128, storeType: "nvarchar"));
            DropColumn("dbo.Product", "Status");
            DropColumn("dbo.Product", "Type");
            DropColumn("dbo.Product", "Name");
        }
    }
}
