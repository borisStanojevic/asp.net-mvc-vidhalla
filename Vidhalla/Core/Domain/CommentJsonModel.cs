using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidhalla.Core.Domain
{
    public class CommentJsonModel
    {
        public string Content { get; set; }
        public int VideoId { get; set; }
        public int CommenterId { get; set; }

    }
}