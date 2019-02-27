using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public void Add(string content, int videoId, int commenterId)
        {
            const string query = "INSERT INTO Comments(Content, DatePosted, IsDeleted, Video_Id, Commenter_Id) "
                               + "VALUES({0}, {1}, {2}, {3}, {4})";

            DbContext.Set<Comment>().SqlQuery(query, content.Length, false, DateTime.Now, videoId, commenterId);
        }

    }
}