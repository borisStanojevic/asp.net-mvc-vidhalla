using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidhalla.Core.Domain
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = "";
        public DateTime DatePosted { get; set; }
        public bool IsDeleted { get; set; }
        public Account Commenter { get; set; }
        public virtual Video Video { get; set; }
        public ICollection<CommentVote> Votes { get; set; }


        public override bool Equals(object obj)
        {
            Comment comment = obj as Comment;
            return (comment != null && comment.Id == Id);
        }

        public override string ToString()
        {
            return $"Id: {Id} Content: {(Content.Length <= 25 ? Content : Content.Substring(0, 22) + "...")}"
                   +
                   $" posted by {Commenter.Username}";
        }
    }
}