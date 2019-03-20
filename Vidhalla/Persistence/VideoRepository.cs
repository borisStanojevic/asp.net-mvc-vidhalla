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


        public IEnumerable<Video> GetAllByViews(SortingDirection sortingDirection, string searchString)
        {
            if (SortingDirection.DESC.Equals(sortingDirection))
                return DbContext.Set<Video>().Include(v => v.Uploader)
                                             .Where(v => v.Title.Contains(searchString) || v.Description.Contains(searchString) || v.Uploader.Username.Contains(searchString))
                                             .OrderByDescending(v => v.ViewsCount)
                                             .ToList();

            return DbContext.Set<Video>().Include(v => v.Uploader)
                                         .Where(v => v.Title.Contains(searchString) || v.Description.Contains(searchString) || v.Uploader.Username.Contains(searchString))
                                         .OrderBy(v => v.Title)
                                         .ToList();
        }

        public IEnumerable<Video> GetAllByTitle(SortingDirection sortingDirection, string searchString)
        {
            if (SortingDirection.DESC == sortingDirection)
                return DbContext.Set<Video>().Include(v => v.Uploader)
                                             .Where(v => v.Title.Contains(searchString) || v.Description.Contains(searchString) || v.Uploader.Username.Contains(searchString))
                                             .OrderByDescending(v => v.Title)
                                             .ToList();

            return DbContext.Set<Video>().Include(v => v.Uploader)
                                         .Where(v => v.Title.Contains(searchString) || v.Description.Contains(searchString) || v.Uploader.Username.Contains(searchString))
                                         .OrderBy(v => v.Title)
                                         .ToList();
        }

        public IEnumerable<Video> GetAllByUploader(SortingDirection sortingDirection, string searchString)
        {
            if (SortingDirection.DESC == sortingDirection)
                return DbContext.Set<Video>().Include(v => v.Uploader)
                                             .Where(v => v.Title.Contains(searchString) || v.Description.Contains(searchString) || v.Uploader.Username.Contains(searchString))
                                             .OrderByDescending(v => v.Uploader.Username)
                                             .ToList();

            return DbContext.Set<Video>().Include(v => v.Uploader)
                                         .Where(v => v.Title.Contains(searchString) || v.Description.Contains(searchString) || v.Uploader.Username.Contains(searchString))
                                         .OrderBy(v => v.Uploader.Username)
                                         .ToList();
        }

        public IEnumerable<Video> GetAllByDateUploaded(SortingDirection sortingDirection, string searchString)
        {
            if (SortingDirection.DESC == sortingDirection)
                return DbContext.Set<Video>().Include(v => v.Uploader)
                                             .Where(v => v.Title.Contains(searchString) || v.Description.Contains(searchString) || v.Uploader.Username.Contains(searchString))
                                             .OrderByDescending(v => v.DateUploaded)
                                             .ToList();

            return DbContext.Set<Video>().Include(v => v.Uploader)
                                         .Where(v => v.Title.Contains(searchString) || v.Description.Contains(searchString) || v.Uploader.Username.Contains(searchString))
                                         .OrderBy(v => v.DateUploaded)
                                         .ToList();
        }


        public Video GetIncludeRelated(int id)
        {
            return DbContext.Set<Video>().Where(v => v.Id == id)
                                         .Include(v => v.Uploader)
                                         .Include(v => v.Votes.Select(vv => vv.Owner))
                                         .Include(v => v.Comments.Select(c => c.Commenter))
                                         .SingleOrDefault();
        }


        public override Video Get(int id)
        {
            return DbContext.Set<Video>().Where(v => v.Id == id)
                                         .Include(v => v.Uploader)
                                         .SingleOrDefault();
        }
    }
}