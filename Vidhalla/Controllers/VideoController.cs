using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidhalla.Core;
using Vidhalla.Core.Domain;
using Vidhalla.Persistence;

namespace Vidhalla.Controllers
{
    public class VideoController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public VideoController()
        {
            _unitOfWork = new UnitOfWork(new VidhallaDbContext());

        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }


        // GET: Video
        public ActionResult Index()
        {
            return View();
        }
    }
}