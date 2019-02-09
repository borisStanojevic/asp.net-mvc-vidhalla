namespace Vidhalla.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateVideos : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Videos "
                + "(Url, Title, Description, Visibility, IsBlocked, IsDeleted, IsCommentingAllowed, IsRatingVisible, ViewsCount, DatePosted, Uploader_Id) "
                + "VALUES "
                + "('https://www.youtube.com/embed/rrVDATvUitA?rel=0&amp;showinfo=0', 'Prvi Video', 'Ovo je prvi video', 1, 0, 0, 1, 1, 24, '2018-01-01', 2)");
            Sql("INSERT INTO Videos "
                + "(Url, Title, Description, Visibility, IsBlocked, IsDeleted, IsCommentingAllowed, IsRatingVisible, ViewsCount, DatePosted, Uploader_Id) "
                + "VALUES "
                + "('https://www.youtube.com/embed/FLwD60hPK4I?rel=0&amp;showinfo=0', 'Drugi Video', 'Ovo je drugi video', 1, 0, 0, 1, 1, 280, '2017-06-01', 2)");
            Sql("INSERT INTO Videos "
                + "(Url, Title, Description, Visibility, IsBlocked, IsDeleted, IsCommentingAllowed, IsRatingVisible, ViewsCount, DatePosted, Uploader_Id) "
                + "VALUES "
                + "('https://www.youtube.com/embed/iPLpiyX6sSY?rel=0&amp;showinfo=0', 'Treci Video', 'Ovo je treci video', 1, 0, 0, 1, 1, 14402, '2018-01-01', 2)");
            Sql("INSERT INTO Videos "
                + "(Url, Title, Description, Visibility, IsBlocked, IsDeleted, IsCommentingAllowed, IsRatingVisible, ViewsCount, DatePosted, Uploader_Id) "
                + "VALUES "
                + "('https://www.youtube.com/embed/qEja72NSg5Q?rel=0&amp;showinfo=0', 'Cetvrti Video', 'Ovo je cetvrti video', 1, 0, 0, 1, 1, 111399, '2011-07-10', 3)");
            Sql("INSERT INTO Videos "
                + "(Url, Title, Description, Visibility, IsBlocked, IsDeleted, IsCommentingAllowed, IsRatingVisible, ViewsCount, DatePosted, Uploader_Id) "
                + "VALUES "
                + "('https://www.youtube.com/embed/KjNe9fuqQ8o?rel=0&amp;showinfo=0', 'Peti Video', 'Ovo je peti video', 3, 0, 0, 1, 1, 2, '2018-12-12', 3)");
            Sql("INSERT INTO Videos "
                + "(Url, Title, Description, Visibility, IsBlocked, IsDeleted, IsCommentingAllowed, IsRatingVisible, ViewsCount, DatePosted, Uploader_Id) "
                + "VALUES "
                + "('https://www.youtube.com/embed/QE3D3TEZDD4?rel=0&amp;showinfo=0', 'Sesti Video', 'Ovo je sesti video', 1, 0, 0, 1, 1, 212, '2017-06-01', 4)");
            Sql("INSERT INTO Videos "
                + "(Url, Title, Description, Visibility, IsBlocked, IsDeleted, IsCommentingAllowed, IsRatingVisible, ViewsCount, DatePosted, Uploader_Id) "
                + "VALUES "
                + "('https://www.youtube.com/embed/dMNxiBCKvMU?rel=0&amp;showinfo=0', 'Sedmi Video', 'Ovo je sedmi video', 2, 0, 0, 1, 1, 31, '2009-11-12', 4)");
        }
        
        public override void Down()
        {
        }
    }
}
