namespace Vidhalla.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateVideoVotes : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO VideoVotes (Video_Id, Owner_Id, Type, DateCreated) VALUES (1, 1, 1, '2018-01-01')");
            Sql("INSERT INTO VideoVotes (Video_Id, Owner_Id, Type, DateCreated) VALUES (1, 2, 1, '2018-01-01')");
            Sql("INSERT INTO VideoVotes (Video_Id, Owner_Id, Type, DateCreated) VALUES (1, 3, 1, '2018-01-01')");
            Sql("INSERT INTO VideoVotes (Video_Id, Owner_Id, Type, DateCreated) VALUES (3, 4, 0, '2018-01-01')");
            Sql("INSERT INTO VideoVotes (Video_Id, Owner_Id, Type, DateCreated) VALUES (2, 2, 1, '2018-01-01')");
        }

        public override void Down()
        {
        }
    }
}
