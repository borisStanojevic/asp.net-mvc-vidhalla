using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Vidhalla.Core.Domain;
using Vidhalla.Persistence;
using Vidhalla.ViewModels.Videos;

namespace Vidhalla.Controllers
{
    public class VideosController : MyController
    {
        //Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        /*  if (!Directory.Exists(target)) 
            {
                Directory.CreateDirectory(target);
            }
        */

        // GET: Videos?sortOrder='key-direction'
        public ActionResult Index(string sortOrder = "dateUploaded-desc")
        {
            string sortingKey;
            SortingDirection sortingDirection;

            try
            {
                sortingKey = sortOrder.Split('-')[0];
                sortingDirection = sortOrder.Split('-')[1].Equals("desc") ? SortingDirection.DESC : SortingDirection.ASC;
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }


            IEnumerable<Video> videos;
            switch (sortingKey)
            {
                case "views":
                    videos = UnitOfWork.Videos.GetAllByViews(sortingDirection);
                    break;
                case "title":
                    videos = UnitOfWork.Videos.GetAllByTitle(sortingDirection);
                    break;
                case "uploader":
                    videos = UnitOfWork.Videos.GetAllByUploader(sortingDirection);
                    break;
                default:
                    videos = UnitOfWork.Videos.GetAllByDateUploaded(sortingDirection);
                    break;

            }
            ICollection<IndexViewModel> viewModel = videos.Select(video => new IndexViewModel(video)).ToList();

            return View(viewModel);
        }

        // GET: Videos/Details/5
        public ActionResult Details(int id)
        {
            Video video = UnitOfWork.Videos.GetIncludeRelated(id);
            if (video == null)
                return HttpNotFound();

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
            Video video = UnitOfWork.Videos.Get(id);

            if (video == null)
                return HttpNotFound();

            return View(video);
        }

        // POST: Videos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Video video)
        {
            if (!ModelState.IsValid)
                return View(video);

            Video videoToUpdate = UnitOfWork.Videos.Get(video.Id);

            string[] valuesToUpdate = {"Title", "Description", "Visibility",
                                       "IsBlocked", "IsCommentingAllowed", "IsRatingVisible"
                                      };
            TryUpdateModel(videoToUpdate, "", valuesToUpdate);
            UnitOfWork.SaveChanges();
            return RedirectToAction("Details", new { id = videoToUpdate.Id });

        }

        // POST: Videos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Video videoToDelete = UnitOfWork.Videos.GetIncludeRelated(id);
            try
            {
                UnitOfWork.Videos.Delete(videoToDelete);
                UnitOfWork.SaveChanges();
            }
            catch(DataException)
            {
            }

            return RedirectToAction("Index");
        }
    }
}
