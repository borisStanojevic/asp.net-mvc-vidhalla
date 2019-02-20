using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vidhalla.Core.Domain;

namespace Vidhalla.Core.Repositories
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Account GetIncludeRelated(string username);
        IEnumerable<Account> GetSubscribeds(int id);
    }
}
