using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidhalla.Core.Domain
{
    public class Account
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^\w{6,31}$", ErrorMessage = "Username can contain letters, digits and underscore and must be between 6 and 32 characters long")]
        public string Username { get; set; }

        [Required]
        [StringLength(127)]
        public string Password { get; set; }
        public string ProfilePicture { get; set; } = "default.png";

        [Display(Description = "First Name")]
        [StringLength(31)]
        public string FirstName { get; set; } = "";

        [Display(Description = "Last Name")]
        [StringLength(63)]
        public string LastName { get; set; } = "";

        [Display(Description = "Channel Description")]
        [StringLength(255)]
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