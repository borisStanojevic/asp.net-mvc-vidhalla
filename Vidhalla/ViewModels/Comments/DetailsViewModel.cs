using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidhalla.ViewModels.Comments
{
    public class DetailsViewModel
    {
        public int Id { get; set; }
        public string CommenterProfilePicture { get; set; }
        public string CommenterUsername { get; set; }
        public string DatePosted { get; set; }
        public string Content { get; set; }

    }
}