using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vidhalla.Core.Domain
{
    public class VideoVote
    {

        public Vote Type { get; set; }
        public DateTime DateCreated { get; set; }

        public int Video_Id { get; set; }
        [ForeignKey("Video_Id")]
        public virtual Video Video { get; set; }

        public int Owner_Id { get; set; }
        [ForeignKey("Owner_Id")]
        public virtual Account Owner { get; set; }

    }
}