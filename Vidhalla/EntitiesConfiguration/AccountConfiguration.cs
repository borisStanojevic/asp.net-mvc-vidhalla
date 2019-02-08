using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Vidhalla.Core.Domain;

namespace Vidhalla.EntitiesConfiguration
{
    public class AccountConfiguration : EntityTypeConfiguration<Account>
    {
        public AccountConfiguration()
        {
            Property(a => a.Username).HasMaxLength(31).IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                                     new IndexAnnotation(new IndexAttribute() {IsUnique = true}));
            Property(a => a.FirstName).HasMaxLength(31);
            Property(a => a.LastName).HasMaxLength(63);
            Property(a => a.ChannelDescription).HasMaxLength(255);
            Property(a => a.DateRegistered).HasColumnType("DATETIME2");

            //Korisnik moze da uploaduje N videa, svaki video ima tacno jednog uploadera
            HasMany(a => a.UploadedVideos).WithRequired(v => v.Uploader).WillCascadeOnDelete(false);
            //Korisnik moze da objavi N komentara, svaki komentar ima objavljivaca
            HasMany(a => a.PostedComments).WithRequired(c => c.Commenter).WillCascadeOnDelete(false);
            //Svaki korisnik moze da se pretplati na N kanala(drugi korisnika) sto bi bila rekurzivna many to many veza
            HasMany(a => a.Subscribers).WithMany().Map(m =>
            {
                m.ToTable("Subscriptions");
                m.MapLeftKey("Subscribing_User_Id");
                m.MapRightKey("Subscribed_User_Id");
            });
            HasMany(a => a.VideoVotes).WithRequired(vv => vv.Owner).WillCascadeOnDelete(true);
            HasMany(a => a.CommentVotes).WithRequired(cv => cv.Owner).WillCascadeOnDelete(true);
        }
    }
}