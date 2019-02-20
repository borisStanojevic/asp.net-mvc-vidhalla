using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vidhalla.Core.Domain;
using Vidhalla.Persistence;

namespace Vidhalla.Controllers
{
    public class MyController : Controller
    {
        protected readonly UnitOfWork UnitOfWork;

        public MyController()
        {
            UnitOfWork = new UnitOfWork(new VidhallaDbContext());
        }

        protected HttpStatusCodeResult HttpBadRequest()
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        protected override void Dispose(bool disposing)
        {
            UnitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}