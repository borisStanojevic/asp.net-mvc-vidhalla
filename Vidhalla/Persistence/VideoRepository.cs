using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Vidhalla.Core.Domain;
using Vidhalla.Core.Repositories;

namespace Vidhalla.Persistence
{
    public class VideoRepository : GenericRepository<Video>, IVideoRepository
    {
        public VideoRepository(DbContext concreteContext) : base(concreteContext)
        {
        }

        public VidhallaDbContext DbContext
        {
            get { return GenericContext as VidhallaDbContext; }
        }


        public override IEnumerable<Video> GetAll()
        {
            return DbContext.Set<Video>().Include(v => v.Uploader)
                                         .Include(v => v.Votes)
                                         .ToList();
        }

        public override Video Get(int id)
        {
            return DbContext.Set<Video>().Where(v => v.Id == id)
                .Include(v => v.Uploader)
                .Include(v => v.Votes)
                .Include(v => v.Comments.Select(c => c.Commenter))
                .Include(v => v.Comments.Select(c => c.Votes))
                .Single();
        }
    }
}