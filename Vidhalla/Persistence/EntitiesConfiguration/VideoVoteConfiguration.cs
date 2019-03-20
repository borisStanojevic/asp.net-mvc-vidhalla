using System.Data.Entity.ModelConfiguration;
using Vidhalla.Core.Domain;

namespace Vidhalla.Persistence.EntitiesConfiguration
{
    public class VideoVoteConfiguration : EntityTypeConfiguration<VideoVote>
    {
        public VideoVoteConfiguration()
        {
            Property(vv => vv.Video_Id).HasColumnOrder(1);
            Property(vv => vv.Owner_Id).HasColumnOrder(2);
            HasKey(vv => new { vv.Video_Id, vv.Owner_Id });

            Property(vv => vv.Type).HasColumnType("TINYINT");
            Property(vv => vv.DateCreated).HasColumnType("DATETIME2");

            HasRequired(vv => vv.Video).WithMany(v => v.Votes);
            HasRequired(vv => vv.Owner).WithMany(u => u.VideoVotes);
        }
    }
}