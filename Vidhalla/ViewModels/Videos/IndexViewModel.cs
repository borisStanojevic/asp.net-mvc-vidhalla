using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidhalla.Core.Domain;

namespace Vidhalla.ViewModels.Videos
{
    public class IndexViewModel
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public int Id { get; set; }
        public string UploaderProfilePicture { get; set; }
        public string UploaderUsername { get; set; }
        public string DateUploaded { get; set; }
        public int ViewsCount { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }

        public IndexViewModel()
        {
            
        }

        public IndexViewModel(Video v)
        {
            Url = v.Url;
            Title = v.Title;
            Id = v.Id;
            UploaderProfilePicture = v.Uploader.ProfilePicture;
            UploaderUsername = v.Uploader.Username;
            DateUploaded = v.DateUploaded.Date.ToShortDateString();
            ViewsCount = v.ViewsCount;
            LikesCount = v.Votes.Where(vv => vv.Type == Vote.LIKE).ToList().Count();
            DislikesCount = v.Votes.Where(vv => vv.Type == Vote.LIKE).ToList().Count();
        }
    }
}
