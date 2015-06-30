namespace Sinq.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActivityOptionalDueDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Activities", "DueDate", c => c.DateTimeOffset(precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Activities", "DueDate", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
    }
}
