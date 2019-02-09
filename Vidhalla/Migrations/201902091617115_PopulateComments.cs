namespace Vidhalla.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateComments : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Comments "
                + "(Content, DatePosted, IsDeleted, Video_Id, Commenter_Id) "
                + "VALUES "
                + "('Delightful unreserved impossible few estimating men favourable see entreaties. She propriety immediate was improving. He or entrance humoured likewise moderate. Much nor game son say feel. Fat make met can must form into gate. Me we offending prevailed discovery.', "
                + "'2018-01-01', 0, 1, 2)");

            Sql("INSERT INTO Comments "
                + "(Content, DatePosted, IsDeleted, Video_Id, Commenter_Id) "
                + "VALUES "
                + "('Name were we at hope. Remainder household direction zealously the unwilling bed sex. Lose and gay ham sake met that. Stood her place one ten spoke yet. Head case knew ever set why over. Marianne returned of peculiar replying in moderate. Roused get enable garret estate old county. Entreaties you devonshire law dissimilar terminated.', "
                + "'2018-01-01', 0, 1, 3)");

            Sql("INSERT INTO Comments "
                + "(Content, DatePosted, IsDeleted, Video_Id, Commenter_Id) "
                + "VALUES "
                + "('Lose eyes get fat shew. Winter can indeed letter oppose way change tended now. So is improve my charmed picture exposed adapted demands. Received had end produced prepared diverted strictly off man branched. Known ye money so large decay voice there to. Preserved be mr cordially incommode as an. He doors quick child an point at. Had share vexed front least style off why him.', "
                + "'2018-02-01', 0, 1, 4)");

            Sql("INSERT INTO Comments "
                + "(Content, DatePosted, IsDeleted, Video_Id, Commenter_Id) "
                + "VALUES "
                + "('Lose eyes get fat shew. Winter can indeed letter oppose way change tended now. So is improve my charmed picture exposed adapted demands. Received had end produced prepared diverted strictly off man branched. Known ye money so large decay voice there to. Preserved be mr cordially incommode as an. He doors quick child an point at. Had share vexed front least style off why him.', "
                + "'2018-02-01', 0, 2, 2)");

            Sql("INSERT INTO Comments "
                + "(Content, DatePosted, IsDeleted, Video_Id, Commenter_Id) "
                + "VALUES "
                + "('Lose eyes get fat shew. Winter can indeed letter oppose way change tended now. So is improve my charmed picture exposed adapted demands. Received had end produced prepared diverted strictly off man branched. Known ye money so large decay voice there to. Preserved be mr cordially incommode as an. He doors quick child an point at. Had share vexed front least style off why him.', "
                + "'2018-02-01', 0, 2, 4)");

            Sql("INSERT INTO Comments "
                + "(Content, DatePosted, IsDeleted, Video_Id, Commenter_Id) "
                + "VALUES "
                + "('Lose eyes get fat shew. Winter can indeed letter oppose way change tended now. So is improve my charmed picture exposed adapted demands. Received had end produced prepared diverted strictly off man branched. Known ye money so large decay voice there to. Preserved be mr cordially incommode as an. He doors quick child an point at. Had share vexed front least style off why him.', "
                + "'2018-02-01', 0, 3, 3)");

            Sql("INSERT INTO Comments "
                + "(Content, DatePosted, IsDeleted, Video_Id, Commenter_Id) "
                + "VALUES "
                + "('Lose eyes get fat shew. Winter can indeed letter oppose way change tended now. So is improve my charmed picture exposed adapted demands. Received had end produced prepared diverted strictly off man branched. Known ye money so large decay voice there to. Preserved be mr cordially incommode as an. He doors quick child an point at. Had share vexed front least style off why him.', "
                + "'2018-02-01', 0, 7, 4)");

            Sql("INSERT INTO Comments "
                + "(Content, DatePosted, IsDeleted, Video_Id, Commenter_Id) "
                + "VALUES "
                + "('Lose eyes get fat shew. Winter can indeed letter oppose way change tended now. So is improve my charmed picture exposed adapted demands. Received had end produced prepared diverted strictly off man branched. Known ye money so large decay voice there to. Preserved be mr cordially incommode as an. He doors quick child an point at. Had share vexed front least style off why him.', "
                + "'2018-02-01', 0, 7, 2)");

            Sql("INSERT INTO Comments "
                + "(Content, DatePosted, IsDeleted, Video_Id, Commenter_Id) "
                + "VALUES "
                + "('Lose eyes get fat shew. Winter can indeed letter oppose way change tended now. So is improve my charmed picture exposed adapted demands. Received had end produced prepared diverted strictly off man branched. Known ye money so large decay voice there to. Preserved be mr cordially incommode as an. He doors quick child an point at. Had share vexed front least style off why him.', "
                + "'2018-02-01', 0, 5, 5)");

        }
        
        public override void Down()
        {
        }
    }
}
