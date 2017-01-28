namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Post_Reported_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Reported", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Reported");
        }
    }
}
