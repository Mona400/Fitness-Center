namespace FitnessCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetailsModels", "ItemName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetailsModels", "ItemName");
        }
    }
}
