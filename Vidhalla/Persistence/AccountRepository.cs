using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Vidhalla.Core.Domain;
using Vidhalla.Core.Repositories;

namespace Vidhalla.Persistence
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(DbContext concreteContext) : base(concreteContext)
        {
        }

        public VidhallaDbContext DbContext
        {
            get { return GenericContext as VidhallaDbContext; }
        }

        public Account GetIncludeRelated(string username)
        {
            return DbContext.Set<Account>().Where(a => a.Username.Equals(username))
                .Include(a => a.UploadedVideos)
                .Include(a => a.PostedComments)
                .Include(a => a.Subscribers)
                .Include(a => a.VideoVotes)
                .SingleOrDefault();
        }

        public Account GetIncludeSubscribers(int id)
        {
            return DbContext.Set<Account>().Where(a => a.Id == id)
                                           .Include(a => a.Subscribers)
                                           .SingleOrDefault();
        }

        public IEnumerable<Account> GetSubscribeds(int id)
        {
            //var account = DbContext.Set<Account>().Find(id);
            const string query = @"SELECT * FROM Accounts WHERE Id IN "
                               + "(SELECT Subscribing_User_Id From Subscriptions "
                               + "WHERE Subscribed_User_Id = {0})";
            return DbContext.Set<Account>().SqlQuery(query, id).ToList();
        }
    }
}