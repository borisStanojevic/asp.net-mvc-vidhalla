using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidhalla.Core.Domain;
using Vidhalla.Persistence;

namespace Vidhalla.Controllers
{
    public class VideosController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public VideosController()
        {
            _unitOfWork = new UnitOfWork(new VidhallaDbContext());

        }

        //Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        /*  if (!Directory.Exists(target)) 
            {
                Directory.CreateDirectory(target);
            }
        */

        // GET: Videos
        public ActionResult Index()
        {
            return View();
        }

        // GET: Videos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Videos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Videos/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Videos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Videos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Videos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Videos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
