namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Payment_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payment", "Description", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payment", "Description");
        }
    }
}
