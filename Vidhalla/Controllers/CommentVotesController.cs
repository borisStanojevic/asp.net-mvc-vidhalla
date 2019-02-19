using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vidhalla.Controllers
{
    public class CommentVotesController : MyController
    {
        // GET: CommentVotes
        public ActionResult Index()
        {
            return View();
        }
    }
}