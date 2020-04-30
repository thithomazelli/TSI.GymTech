namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Database_PaymentProduct : DbMigration
    {
        public override void Up()
        {
            AlterColumn("PaymentProduct", "Quantity", c => c.Int(nullable: false));
            AlterColumn("PaymentProduct", "UnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("PaymentProduct", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("PaymentProduct", "TotalPrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("PaymentProduct", "UnitPrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("PaymentProduct", "Quantity", c => c.Int());
        }
    }
}
