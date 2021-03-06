﻿using System.Data.Entity.ModelConfiguration;
using Vidhalla.Core.Domain;

namespace Vidhalla.Persistence.EntitiesConfiguration
{
    public class VideoConfiguration : EntityTypeConfiguration<Video>
    {
        public VideoConfiguration()
        {
            Property(v => v.Url).IsRequired();
            Property(v => v.Title).IsRequired().HasMaxLength(127);
            Property(v => v.Description).HasMaxLength(1023);
            Property(v => v.DateUploaded).HasColumnType("DATETIME2");

            HasRequired(v => v.Uploader).WithMany(a => a.UploadedVideos);
            HasMany(v => v.Comments).WithRequired(c => c.Video);
            HasMany(v => v.Votes).WithRequired(vv => vv.Video).WillCascadeOnDelete();
        }
    }
}