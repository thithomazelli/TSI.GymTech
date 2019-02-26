namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_SheetQuestion_And_SheetAwser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SheetAnswer", "CreateDate", c => c.DateTime(precision: 0));
            AddColumn("dbo.SheetAnswer", "CreateUserId", c => c.Int(nullable: false));
            AddColumn("dbo.SheetAnswer", "ModifyDate", c => c.DateTime(precision: 0));
            AddColumn("dbo.SheetAnswer", "ModifyUserId", c => c.Int(nullable: false));
            AddColumn("dbo.SheetQuestion", "CreateDate", c => c.DateTime(precision: 0));
            AddColumn("dbo.SheetQuestion", "CreateUserId", c => c.Int(nullable: false));
            AddColumn("dbo.SheetQuestion", "ModifyDate", c => c.DateTime(precision: 0));
            AddColumn("dbo.SheetQuestion", "ModifyUserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SheetQuestion", "ModifyUserId");
            DropColumn("dbo.SheetQuestion", "ModifyDate");
            DropColumn("dbo.SheetQuestion", "CreateUserId");
            DropColumn("dbo.SheetQuestion", "CreateDate");
            DropColumn("dbo.SheetAnswer", "ModifyUserId");
            DropColumn("dbo.SheetAnswer", "ModifyDate");
            DropColumn("dbo.SheetAnswer", "CreateUserId");
            DropColumn("dbo.SheetAnswer", "CreateDate");
        }
    }
}
