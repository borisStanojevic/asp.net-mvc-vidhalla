namespace Vidhalla.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateAccounts : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Accounts " +
                "(Username, Password, ProfilePicture, FirstName, LastName, ChannelDescription, Role, DateRegistered, IsBlocked, IsDeleted)"
                + " VALUES "
                + "('mitar123', '123456', 'default.png', 'Mitar', 'Miric', 'Ovo je kanal Mitra Mirica', 1, '2012-01-01', 0, 0)");

            Sql("INSERT INTO Accounts " +
                "(Username, Password, ProfilePicture, FirstName, LastName, ChannelDescription, Role, DateRegistered, IsBlocked, IsDeleted)"
                + " VALUES "
                + "('jovan123', '123456', 'default.png', 'Jovan', 'Jovanovic', 'Ovo je kanal Jovana Jovanovica', 2, '2012-01-01', 0, 0)");

            Sql("INSERT INTO Accounts " +
                "(Username, Password, ProfilePicture, FirstName, LastName, ChannelDescription, Role, DateRegistered, IsBlocked, IsDeleted)"
                + " VALUES "
                + "('pero123', '123456', 'default.png', 'Petar', 'Petrovic', 'Ovo je kanal Petra Petrovica', 2, '2012-01-01', 0, 0)");

            Sql("INSERT INTO Accounts " +
                "(Username, Password, ProfilePicture, FirstName, LastName, ChannelDescription, Role, DateRegistered, IsBlocked, IsDeleted)"
                + " VALUES "
                + "('saban123', '123456', 'default.png', 'Saban', 'Sabanovic', 'Ovo je kanal Sabana Sabanovica', 1, '2012-01-01', 0, 0)");
        }

        public override void Down()
        {
        }
    }
}
