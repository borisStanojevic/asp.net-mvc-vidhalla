using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidhalla.Core.Domain;

namespace Vidhalla.ViewModels.Accounts
{
    public class DetailsViewModel
    {
        public Account Account { get; set; }
        public IEnumerable<Account> Subscribeds { get; set; }

        public DetailsViewModel(Account account, IEnumerable<Account> subscribeds)
        {
            Account = account;
            Subscribeds = subscribeds;
        }
    }
}