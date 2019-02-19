using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidhalla.Core.Domain
{
    public enum Vote : byte
    {
        DISLIKE = 0,
        LIKE    = 1
    }

    public enum Visibility : byte
    {
        PUBLIC   = 1,
        UNLISTED = 2,
        PRIVATE  = 3
    }

    public enum Role : byte
    {
        ADMIN        = 1,
        REGULAR_USER = 2
    }

    public enum SortingDirection
    {
        ASC,
        DESC
    }
}