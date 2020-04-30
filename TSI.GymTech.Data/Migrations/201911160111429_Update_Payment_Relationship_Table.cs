namespace TSI.GymTech.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Payment_Relationship_Table : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("Payment", "PersonId", "Person");
            //DropColumn("Payment", "PersonId");
            AddColumn("Payment", "StudentId", c => c.Int());
            AddForeignKey("Payment", "StudentId", "Person", "PersonId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "Payment", name: "IX_StudentId", newName: "IX_PersonId");
            // RenameColumn(table: "Payment", name: "StudentId", newName: "PersonId");
        }
    }
}
