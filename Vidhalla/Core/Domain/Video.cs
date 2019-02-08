using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidhalla.Core.Domain
{
    public class Video
    {
        private const string URL_BASE = "https://www.youtube.com/embed/";

        public int Id { get; set; }
        private string _url;
        public string Url
        {
            get => _url;
            set => _url = URL_BASE + value;
        }
        public string Description { get; set; } = "";
        public Visibility Visibility { get; set; } = Visibility.PUBLIC;
        public bool IsBlocked { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsCommentingAllowed { get; set; } = true;
        public bool IsRatingVisible { get; set; } = true;
        public int ViewsCount { get; set; }
        public DateTime DatePosted { get; set; }
        public Account Uploader { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<VideoVote> Votes { get; set; }


        public Video()
        {
        }

        public Video(int id, string description, Visibility visibility, bool isCommentingAllowed, bool isRatingVisible, int viewsCount, Account uploader)
        {
            Id = id;

            Description = description;
            Visibility = visibility;
            IsCommentingAllowed = isCommentingAllowed;
            IsRatingVisible = isRatingVisible;
            ViewsCount = viewsCount;
            DatePosted = new DateTime();
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