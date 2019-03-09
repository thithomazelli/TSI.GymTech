namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Product_Name_Required : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "Name", c => c.String(nullable: false, maxLength: 128, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "Name", c => c.String(maxLength: 128, storeType: "nvarchar"));
        }
    }
}
