﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vidhalla.Controllers
{
    public class CommentsController : MyController
    {
        // GET: Comments
        public ActionResult Index()
        {
            return View();
        }
    }
}