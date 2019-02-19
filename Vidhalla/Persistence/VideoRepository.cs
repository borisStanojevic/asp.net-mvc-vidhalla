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
    public class VideoRepository : GenericRepository<Video>, IVideoRepository
    {
        public VideoRepository(DbContext concreteContext) : base(concreteContext)
        {
        }

        public VidhallaDbContext DbContext
        {
            get { return GenericContext as VidhallaDbContext; }
        }


        public IEnumerable<Video> GetAllByViews(SortingDirection sortingDirection)
        {
            if (SortingDirection.DESC.Equals(sortingDirection))
                return DbContext.Set<Video>().Include(v => v.Uploader)
                                             .OrderByDescending(v => v.ViewsCount)
                                             .ToList();

            return DbContext.Set<Video>().Include(v => v.Uploader)
                                         .OrderBy(v => v.Title)
                                         .ToList();
        }

        public IEnumerable<Video> GetAllByTitle(SortingDirection sortingDirection)
        {
            if (SortingDirection.DESC.Equals(sortingDirection))
                return DbContext.Set<Video>().Include(v => v.Uploader)
                                             .OrderByDescending(v => v.Title)
                                             .ToList();

            return DbContext.Set<Video>().Include(v => v.Uploader)
                                         .OrderBy(v => v.Title)
                                         .ToList();
        }

        public IEnumerable<Video> GetAllByUploader(SortingDirection sortingDirection)
        {
            if (SortingDirection.DESC.Equals(sortingDirection))
                return DbContext.Set<Video>().Include(v => v.Uploader)
                                             .OrderByDescending(v => v.Uploader.Username)
                                             .ToList();

            return DbContext.Set<Video>().Include(v => v.Uploader)
                                         .OrderBy(v => v.Uploader.Username)
                                         .ToList();
        }

        public IEnumerable<Video> GetAllByDateUploaded(SortingDirection sortingDirection)
        {
            if (SortingDirection.DESC.Equals(sortingDirection))
                return DbContext.Set<Video>().Include(v => v.Uploader)
                                             .OrderByDescending(v => v.DateUploaded)
                                             .ToList();

            return DbContext.Set<Video>().Include(v => v.Uploader)
                                         .OrderBy(v => v.DateUploaded)
                                         .ToList();
        }


        public Video GetIncludeRelated(int id)
        {
            return DbContext.Set<Video>().Where(v => v.Id == id)
                                         .Include(v => v.Uploader)
                                         .Include(v => v.Votes)
                                         .Include(v => v.Comments.Select(c => c.Commenter))
                                         .Include(v => v.Comments.Select(c => c.Votes))
                                         .SingleOrDefault();
        }
    }
}