namespace Vidhalla.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIdFromVoteClasses : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CommentVotes", "Id");
            DropColumn("dbo.VideoVotes", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VideoVotes", "Id", c => c.Int(nullable: false));
            AddColumn("dbo.CommentVotes", "Id", c => c.Int(nullable: false));
        }
    }
}
