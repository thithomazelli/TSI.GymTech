namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Payment_Table1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Payment", "Description", c => c.String(nullable: false, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Payment", "Description", c => c.String(unicode: false));
        }
    }
}
