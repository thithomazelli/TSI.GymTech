namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removing_Password_To_Person : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Person", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Person", "Password", c => c.String(maxLength: 16, storeType: "nvarchar"));
        }
    }
}
