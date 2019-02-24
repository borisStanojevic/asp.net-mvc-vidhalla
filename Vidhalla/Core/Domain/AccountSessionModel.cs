using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidhalla.Core.Domain
{
    public class AccountSessionModel
    {
        public string Username { get; set; }
        public Role Role { get; set; }
        public bool IsBlocked { get; set; }
    }
}