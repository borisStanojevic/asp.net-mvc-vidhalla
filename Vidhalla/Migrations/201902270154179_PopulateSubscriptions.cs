namespace Vidhalla.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateSubscriptions : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Subscriptions (Subscribing_User_Id, Subscribed_User_Id) VALUES (1,2)");
            Sql("INSERT INTO Subscriptions (Subscribing_User_Id, Subscribed_User_Id) VALUES (2,3)");
            Sql("INSERT INTO Subscriptions (Subscribing_User_Id, Subscribed_User_Id) VALUES (2,4)");
            Sql("INSERT INTO Subscriptions (Subscribing_User_Id, Subscribed_User_Id) VALUES (3,1)");
            Sql("INSERT INTO Subscriptions (Subscribing_User_Id, Subscribed_User_Id) VALUES (4,3)");
            Sql("INSERT INTO Subscriptions (Subscribing_User_Id, Subscribed_User_Id) VALUES (1,3)");
            Sql("INSERT INTO Subscriptions (Subscribing_User_Id, Subscribed_User_Id) VALUES (2,1)");
        }

        public override void Down()
        {
        }
    }
}
