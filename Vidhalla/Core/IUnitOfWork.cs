using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vidhalla.Core.Repositories;

namespace Vidhalla.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IVideoRepository Videos { get; }
        IAccountRepository Accounts { get; }
        ICommentRepository Comments { get; }
        IVideoVoteRepository VideoVotes { get; }

        int SaveChanges();
    }
}
