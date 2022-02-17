namespace FitnessCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddItems : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Items");
            AddPrimaryKey("dbo.Items", "ItemId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Items");
            AddPrimaryKey("dbo.Items", new[] { "ItemId", "CategoryId" });
        }
    }
}
