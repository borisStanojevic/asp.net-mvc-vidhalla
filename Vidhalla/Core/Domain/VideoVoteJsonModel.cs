using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidhalla.Core.Domain
{
    public class VideoVoteJsonModel
    {
        public int VideoId { get; set; }
        public Vote Type { get; set; }
    }
}