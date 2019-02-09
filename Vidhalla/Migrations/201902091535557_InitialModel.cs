namespace Vidhalla.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 31),
                        Password = c.String(nullable: false, maxLength: 127),
                        ProfilePicture = c.String(nullable: false),
                        FirstName = c.String(maxLength: 31),
                        LastName = c.String(maxLength: 63),
                        ChannelDescription = c.String(maxLength: 255),
                        Role = c.Byte(nullable: false),
                        DateRegistered = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsBlocked = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Username, unique: true);
            
            CreateTable(
                "dbo.CommentVotes",
                c => new
                    {
                        Comment_Id = c.Int(nullable: false),
                        Owner_Id = c.Int(nullable: false),
                        Type = c.Byte(nullable: false),
                        DateCreated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => new { t.Comment_Id, t.Owner_Id })
                .ForeignKey("dbo.Comments", t => t.Comment_Id, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.Owner_Id, cascadeDelete: true)
                .Index(t => t.Comment_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 1023),
                        DatePosted = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsDeleted = c.Boolean(nullable: false),
                        Video_Id = c.Int(nullable: false),
                        Commenter_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Videos", t => t.Video_Id, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.Commenter_Id)
                .Index(t => t.Video_Id)
                .Index(t => t.Commenter_Id);
            
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(nullable: false),
                        Title = c.String(nullable: false, maxLength: 127),
                        Description = c.String(maxLength: 1023),
                        Visibility = c.Byte(nullable: false),
                        IsBlocked = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsCommentingAllowed = c.Boolean(nullable: false),
                        IsRatingVisible = c.Boolean(nullable: false),
                        ViewsCount = c.Int(nullable: false),
                        DatePosted = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Uploader_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Uploader_Id)
                .Index(t => t.Uploader_Id);
            
            CreateTable(
                "dbo.VideoVotes",
                c => new
                    {
                        Video_Id = c.Int(nullable: false),
                        Owner_Id = c.Int(nullable: false),
                        Type = c.Byte(nullable: false),
                        DateCreated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => new { t.Video_Id, t.Owner_Id })
                .ForeignKey("dbo.Videos", t => t.Video_Id, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.Owner_Id, cascadeDelete: true)
                .Index(t => t.Video_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        Subscribing_User_Id = c.Int(nullable: false),
                        Subscribed_User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Subscribing_User_Id, t.Subscribed_User_Id })
                .ForeignKey("dbo.Accounts", t => t.Subscribing_User_Id)
                .ForeignKey("dbo.Accounts", t => t.Subscribed_User_Id)
                .Index(t => t.Subscribing_User_Id)
                .Index(t => t.Subscribed_User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VideoVotes", "Owner_Id", "dbo.Accounts");
            DropForeignKey("dbo.Videos", "Uploader_Id", "dbo.Accounts");
            DropForeignKey("dbo.Subscriptions", "Subscribed_User_Id", "dbo.Accounts");
            DropForeignKey("dbo.Subscriptions", "Subscribing_User_Id", "dbo.Accounts");
            DropForeignKey("dbo.Comments", "Commenter_Id", "dbo.Accounts");
            DropForeignKey("dbo.CommentVotes", "Owner_Id", "dbo.Accounts");
            DropForeignKey("dbo.CommentVotes", "Comment_Id", "dbo.Comments");
            DropForeignKey("dbo.VideoVotes", "Video_Id", "dbo.Videos");
            DropForeignKey("dbo.Comments", "Video_Id", "dbo.Videos");
            DropIndex("dbo.Subscriptions", new[] { "Subscribed_User_Id" });
            DropIndex("dbo.Subscriptions", new[] { "Subscribing_User_Id" });
            DropIndex("dbo.VideoVotes", new[] { "Owner_Id" });
            DropIndex("dbo.VideoVotes", new[] { "Video_Id" });
            DropIndex("dbo.Videos", new[] { "Uploader_Id" });
            DropIndex("dbo.Comments", new[] { "Commenter_Id" });
            DropIndex("dbo.Comments", new[] { "Video_Id" });
            DropIndex("dbo.CommentVotes", new[] { "Owner_Id" });
            DropIndex("dbo.CommentVotes", new[] { "Comment_Id" });
            DropIndex("dbo.Accounts", new[] { "Username" });
            DropTable("dbo.Subscriptions");
            DropTable("dbo.VideoVotes");
            DropTable("dbo.Videos");
            DropTable("dbo.Comments");
            DropTable("dbo.CommentVotes");
            DropTable("dbo.Accounts");
        }
    }
}
