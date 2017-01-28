namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Boards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Description = c.String(maxLength: 200),
                        ParentBoardId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Boards", t => t.ParentBoardId)
                .Index(t => t.ParentBoardId);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 200),
                        CreationDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        BoardId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Boards", t => t.BoardId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.BoardId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(nullable: false),
                        Text = c.String(nullable: false),
                        UserId = c.Int(nullable: false),
                        TopicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Topics", t => t.TopicId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TopicId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 30),
                        Email = c.String(nullable: false),
                        Password = c.String(),
                        RegistrationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Description = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Topics", "BoardId", "dbo.Boards");
            DropForeignKey("dbo.Topics", "UserId", "dbo.Users");
            DropForeignKey("dbo.Posts", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.Posts", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRole", "UserId", "dbo.Users");
            DropForeignKey("dbo.Profiles", "Id", "dbo.Users");
            DropForeignKey("dbo.Boards", "ParentBoardId", "dbo.Boards");
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.Profiles", new[] { "Id" });
            DropIndex("dbo.Posts", new[] { "TopicId" });
            DropIndex("dbo.Posts", new[] { "UserId" });
            DropIndex("dbo.Topics", new[] { "BoardId" });
            DropIndex("dbo.Topics", new[] { "UserId" });
            DropIndex("dbo.Boards", new[] { "ParentBoardId" });
            DropTable("dbo.UserRole");
            DropTable("dbo.Roles");
            DropTable("dbo.Profiles");
            DropTable("dbo.Users");
            DropTable("dbo.Posts");
            DropTable("dbo.Topics");
            DropTable("dbo.Boards");
        }
    }
}
