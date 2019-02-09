using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Vidhalla.Core.Domain;

namespace Vidhalla.EntitiesConfiguration
{
    public class CommentVoteConfiguration : EntityTypeConfiguration<CommentVote>
    {
        public CommentVoteConfiguration()
        {
            Property(cv => cv.Comment_Id).HasColumnOrder(1);
            Property(cv => cv.Owner_Id).HasColumnOrder(2);
            HasKey(cv => new { cv.Comment_Id, cv.Owner_Id});

            Property(cv => cv.Type).HasColumnType("TINYINT");
            Property(cv => cv.DateCreated).HasColumnType("DATETIME2");

            HasRequired(cv => cv.Comment).WithMany(c => c.Votes);
            HasRequired(cv => cv.Owner).WithMany(u => u.CommentVotes);
        }
    }
}