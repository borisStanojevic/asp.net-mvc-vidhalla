using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vidhalla.ViewModels.Accounts
{
    public class UploadPictureViewModel
    {
        public HttpPostedFileBase Picture { get; set; }
        public int AccountId { get; set; }
    }
}