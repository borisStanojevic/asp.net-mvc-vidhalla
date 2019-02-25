using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidhalla.Core.Domain
{
    public class AccountSessionModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
        public bool IsBlocked { get; set; }

        public bool IsAdmin()
        {
            return Role == Role.ADMIN;
        }

        public bool IsRegularUser()
        {
            return Role == Role.REGULAR_USER;
        }

        public bool Is(Account account)
        {
            return Username.Equals(account.Username) && Id == account.Id;
        }
    }
}