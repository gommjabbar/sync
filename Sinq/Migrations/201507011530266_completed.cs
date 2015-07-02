namespace Sinq.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class completed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "Completed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activities", "CompletedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Activities", "CompletedAt");
            DropColumn("dbo.Activities", "Completed");
        }
    }
}
