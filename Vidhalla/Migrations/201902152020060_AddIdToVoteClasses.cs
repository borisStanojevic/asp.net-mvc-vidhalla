namespace Vidhalla.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIdToVoteClasses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CommentVotes", "Id", c => c.Int(nullable: false));
            AddColumn("dbo.VideoVotes", "Id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VideoVotes", "Id");
            DropColumn("dbo.CommentVotes", "Id");
        }
    }
}
