using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidhalla.Core;
using Vidhalla.Core.Domain;
using Vidhalla.Core.Repositories;

namespace Vidhalla.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VidhallaDbContext _context;

        public UnitOfWork(VidhallaDbContext context)
        {
            _context = context;
            Videos = new VideoRepository(_context);
            Comments = new CommentRepository(_context);
            Accounts = new AccountRepository(_context);
            VideoVotes = new VideoVoteRepository(_context);
        }


        public IVideoRepository Videos { get; }
        public IAccountRepository Accounts { get; }
        public ICommentRepository Comments { get; }
        public IVideoVoteRepository VideoVotes { get; }


        public void Dispose()
        {
            _context.Dispose();
        }

        public int SaveChanges()
        {
           return _context.SaveChanges();
        }

    }
}