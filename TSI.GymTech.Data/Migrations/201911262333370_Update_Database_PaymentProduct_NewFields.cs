namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Database_PaymentProduct_NewFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentProduct", "Description", c => c.String(unicode: false));
            AddColumn("dbo.PaymentProduct", "PaymentType", c => c.Int());
            AddColumn("dbo.PaymentProduct", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.PaymentProduct", "Quota", c => c.Int());
            AlterColumn("dbo.Payment", "Discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PaymentProduct", "Discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PaymentProduct", "Discount", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Payment", "Discount", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.PaymentProduct", "Quota");
            DropColumn("dbo.PaymentProduct", "Status");
            DropColumn("dbo.PaymentProduct", "PaymentType");
            DropColumn("dbo.PaymentProduct", "Description");
        }
    }
}
