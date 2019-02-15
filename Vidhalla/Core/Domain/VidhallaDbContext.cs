using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Vidhalla.EntitiesConfiguration;

namespace Vidhalla.Core.Domain
{
    public class VidhallaDbContext : DbContext
    {
        public static VidhallaDbContext Create()
        {
            return new VidhallaDbContext();
        }

        public VidhallaDbContext() : base("name=VidhallaDbConnection")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        {
            //Ovdje registrujem konfiguracije modela, koje se nalaze u EntitiesConfiguration namespaceu
            base.OnModelCreating(dbModelBuilder);

            dbModelBuilder.Configurations.Add(new AccountConfiguration());
            dbModelBuilder.Configurations.Add(new VideoConfiguration());
            dbModelBuilder.Configurations.Add(new CommentConfiguration());
            dbModelBuilder.Configurations.Add(new VideoVoteConfiguration());
            dbModelBuilder.Configurations.Add(new CommentVoteConfiguration());
        }
    }
}