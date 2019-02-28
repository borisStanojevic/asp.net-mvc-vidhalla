using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Vidhalla.Core.Domain;
using Vidhalla.Core.Repositories;

namespace Vidhalla.Persistence
{
    public class VideoVoteRepository : GenericRepository<VideoVote>, IVideoVoteRepository
    {
        public VideoVoteRepository(DbContext concreteContext) : base(concreteContext)
        {
        }

        public VidhallaDbContext DbContext
        {
            get { return GenericContext as VidhallaDbContext; }
        }

        //public void Add(Vote type, int videoId, int ownerId)
        //{
        //    string query = @"INSERT INTO VideoVotes"
        //    DbContext.Set<VideoVote>().SQ
        //}

        public override VideoVote Get(Expression<Func<VideoVote, bool>> predicate)
        {
            return DbContext.Set<VideoVote>().Include(vv => vv.Owner)
                                             .Include(vv => vv.Video)
                                             .Where(predicate)
                                             .SingleOrDefault();
        }
    }
}