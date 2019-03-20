using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidhalla.Core.Domain
{
    public class Video
    {

        public int Id { get; set; }

        [Required]
        [Url]
        public string Url { get; set; }

        [Required]
        [StringLength(127)]
        public string Title { get; set; }

        [StringLength(1023)]
        public string Description { get; set; } = "";

        [Required]
        public Visibility Visibility { get; set; } = Visibility.PUBLIC;

        public bool IsBlocked { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsCommentingAllowed { get; set; } = true;
        public bool IsRatingVisible { get; set; } = true;
        public int ViewsCount { get; set; }
        public DateTime DateUploaded { get; set; }
        public Account Uploader { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<VideoVote> Votes { get; set; }


        public Video()
        {
            
        }

        public Video(string title, string description, Visibility visibility, bool isCommentingAllowed, bool isRatingVisible, int viewsCount, Account uploader)
        {
            Title = title;
            Description = description;
            Visibility = visibility;
            IsCommentingAllowed = isCommentingAllowed;
            IsRatingVisible = isRatingVisible;
            ViewsCount = viewsCount;
            DateUploaded = new DateTime();
            Uploader = uploader;
        }


        public override bool Equals(object obj)
        {
            Video video = obj as Video;
            return (video != null && video.Id == Id);
        }

        public override string ToString()
        {
            return $"Id: {Id} Url: {Url} uploaded by {Uploader.Username}";
        }
    }
}