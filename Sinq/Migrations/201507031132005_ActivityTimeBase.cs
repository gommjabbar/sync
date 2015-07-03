namespace Sinq.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActivityTimeBase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActivityTimes", "CreateDate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.ActivityTimes", "UpdateDate", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.Activities", "CompletedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Activities", "CompletedAt", c => c.DateTime(nullable: false));
            DropColumn("dbo.ActivityTimes", "UpdateDate");
            DropColumn("dbo.ActivityTimes", "CreateDate");
        }
    }
}
