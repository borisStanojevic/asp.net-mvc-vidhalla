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
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(DbContext concreteContext) : base(concreteContext)
        {
        }

        public VidhallaDbContext DbContext
        {
            get { return GenericContext as VidhallaDbContext; }
        }

        public IEnumerable<Comment> GetAllByDatePosted(Expression<Func<Comment, bool>> predicate, SortingDirection sortingDirection)
        {
            if (SortingDirection.DESC == sortingDirection)
                return DbContext.Set<Comment>().Include(c => c.Commenter)
                                               .Where(predicate)
                                               .OrderByDescending(c => c.DatePosted)
                                               .ToList();

            return DbContext.Set<Comment>().Include(c => c.Commenter)
                                           .Where(predicate)
                                           .OrderBy(c => c.DatePosted)
                                           .ToList();
        }

        public override Comment Get(int id)
        {
            return DbContext.Set<Comment>().Where(c => c.Id == id)
                                           .Include(c => c.Commenter)
                                           .SingleOrDefault();
        }
    }

}