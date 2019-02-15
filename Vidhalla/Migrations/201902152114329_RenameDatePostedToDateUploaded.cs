namespace Vidhalla.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameDatePostedToDateUploaded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Videos", "DateUploaded", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropColumn("dbo.Videos", "DatePosted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Videos", "DatePosted", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropColumn("dbo.Videos", "DateUploaded");
        }
    }
}
