using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidhalla.Core.Domain
{
    public class Account
    {
        public int    Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string ChannelDescription { get; set; } = "";
        public Role Role { get; set; } = Role.REGULAR_USER;
        public DateTime DateRegistered { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Video> UploadedVideos { get; set; }
        public virtual ICollection<Comment> PostedComments { get; set; }
        public virtual ICollection<Account> Subscribers { get; set; }
        public virtual ICollection<VideoVote> VideoVotes { get; set; }
        public virtual ICollection<CommentVote> CommentVotes { get; set; }


        public override bool Equals(object obj)
        {
            Account account = obj as Account;
            return (account != null && account.Id == Id);
        }

        public override string ToString()
        {
            return $"Id: {Id} Username: {Username} Role: {Role.ToString()}";
        }
    }
}