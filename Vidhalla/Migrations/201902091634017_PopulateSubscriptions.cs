namespace Vidhalla.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateSubscriptions : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Subscriptions (Subscribing_User_Id, Subscribed_User_Id) VALUES (2,3)");
            Sql("INSERT INTO Subscriptions (Subscribing_User_Id, Subscribed_User_Id) VALUES (3,4)");
            Sql("INSERT INTO Subscriptions (Subscribing_User_Id, Subscribed_User_Id) VALUES (3,5)");
            Sql("INSERT INTO Subscriptions (Subscribing_User_Id, Subscribed_User_Id) VALUES (4,2)");
            Sql("INSERT INTO Subscriptions (Subscribing_User_Id, Subscribed_User_Id) VALUES (5,4)");
            Sql("INSERT INTO Subscriptions (Subscribing_User_Id, Subscribed_User_Id) VALUES (2,4)");
            Sql("INSERT INTO Subscriptions (Subscribing_User_Id, Subscribed_User_Id) VALUES (3,2)");
        }
        
        public override void Down()
        {
        }
    }
}
