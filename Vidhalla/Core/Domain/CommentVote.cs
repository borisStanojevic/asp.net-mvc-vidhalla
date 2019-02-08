using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vidhalla.Core.Domain
{
    public class CommentVote
    {
        public Vote Type { get; set; }
        public DateTime DateCreated { get; set; }

        public int Comment_Id { get; set; }
        [ForeignKey("Comment_Id")]
        public virtual Comment Comment { get; set; }

        public int Owner_Id { get; set; }
        [ForeignKey("Owner_Id")]
        public virtual Account Owner { get; set; }

    }
}