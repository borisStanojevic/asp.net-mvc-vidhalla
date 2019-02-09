namespace Vidhalla.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCommentVotes : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO CommentVotes (Comment_Id, Owner_Id, Type, DateCreated) VALUES (1, 2, 1, '2018-01-01')");
            Sql("INSERT INTO CommentVotes (Comment_Id, Owner_Id, Type, DateCreated) VALUES (1, 3, 1, '2018-01-01')");
            Sql("INSERT INTO CommentVotes (Comment_Id, Owner_Id, Type, DateCreated) VALUES (1, 4, 1, '2018-01-01')");
            Sql("INSERT INTO CommentVotes (Comment_Id, Owner_Id, Type, DateCreated) VALUES (3, 5, 0, '2018-01-01')");
            Sql("INSERT INTO CommentVotes (Comment_Id, Owner_Id, Type, DateCreated) VALUES (2, 3, 1, '2018-01-01')");
        }
        
        public override void Down()
        {
        }
    }
}
