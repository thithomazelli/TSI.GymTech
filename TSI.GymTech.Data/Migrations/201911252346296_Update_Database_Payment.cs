namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Database_Payment : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Payment", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("Payment", "TotalPrice", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
