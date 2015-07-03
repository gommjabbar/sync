namespace Sinq.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompletedAt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActivityTimes", "CreateDate", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.ActivityTimes", "UpdateDate", c => c.DateTimeOffset(precision: 7));
            DropColumn("dbo.ActivityTimes", "StartTime");
            DropColumn("dbo.ActivityTimes", "StopTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ActivityTimes", "StopTime", c => c.DateTimeOffset(precision: 7));
            AddColumn("dbo.ActivityTimes", "StartTime", c => c.DateTimeOffset(nullable: false, precision: 7));
            DropColumn("dbo.ActivityTimes", "UpdateDate");
            DropColumn("dbo.ActivityTimes", "CreateDate");
        }
    }
}
