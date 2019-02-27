using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Vidhalla.Core.Domain;

namespace Vidhalla.EntitiesConfiguration
{
    public class CommentConfiguration : EntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        {
            Property(c => c.Content).IsRequired().HasMaxLength(1023);
            Property(c => c.DatePosted).HasColumnType("DATETIME2");

            HasRequired(c => c.Commenter).WithMany(a => a.PostedComments);
            HasRequired(c => c.Video).WithMany(v => v.Comments);
        }
    }
}