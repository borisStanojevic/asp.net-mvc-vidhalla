using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Vidhalla.Core.Domain;
using Vidhalla.Persistence;
using Vidhalla.ViewModels.Videos;

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

        // GET: Videos?sortOrder='key-direction'
        public ActionResult Index(string sortOrder)
        {
            string sortingKey;
            SortingDirection sortingDirection;

            //Ako je proslijedjen string za sortiranje onda...
            if (!string.IsNullOrEmpty(sortOrder))
            {
                //Pokusaj da izvadis kljuc i direkciju po kojima se sortira
                //Zasto pokusaj ? Pa ako neko proslijedi ?sortOrder='key|direction' kod ce da pukne
                //jer je delimiter '-'
                try
                {
                    sortingKey = sortOrder.Split('-')[0];
                    sortingDirection = sortOrder.Split('-')[1].Equals("desc") ? SortingDirection.DESC : SortingDirection.ASC;
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "Videos");
                }
            }
            else
            {
                sortingKey = "dateUploaded";
                sortingDirection = SortingDirection.DESC;
            }

            IEnumerable<Video> videos;
            switch (sortingKey)
            {
                case "views":
                    videos = _unitOfWork.Videos.GetAllByViews(sortingDirection);
                    break;
                case "title":
                    videos = _unitOfWork.Videos.GetAllByTitle(sortingDirection);
                    break;
                case "uploader":
                    videos = _unitOfWork.Videos.GetAllByUploader(sortingDirection);
                    break;
                default:
                    videos = _unitOfWork.Videos.GetAllByDateUploaded(sortingDirection);
                    break;

            }
            ICollection<IndexViewModel> viewModel = videos.Select(video => new IndexViewModel(video)).ToList();

            return View(viewModel);
        }

        // GET: Videos/Details/5
        public ActionResult Details(int id)
        {
            Video video = _unitOfWork.Videos.Get(id);

            return View(video);
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
