﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vidhalla.Core.Domain;

namespace Vidhalla.Core.Repositories
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        IEnumerable<Comment> GetAllByDatePosted(Expression<Func<Comment, bool>> predicate, SortingDirection sortingDirection);
    }
}
