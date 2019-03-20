using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Vidhalla.Core.Domain;
using Vidhalla.Filters;
using Vidhalla.Persistence;
using Vidhalla.ViewModels.Videos;
using static Vidhalla.Core.Domain.Role;
using static Vidhalla.Core.Domain.Visibility;

namespace Vidhalla.Controllers
{
    public class VideosController : MyController
    {

        // GET: Videos?sortOrder='key-direction'
        public ActionResult Index(string sortOrder = "dateUploaded-desc", string searchString = "")
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
                    videos = UnitOfWork.Videos.GetAllByViews(sortingDirection, searchString);
                    break;
                case "title":
                    videos = UnitOfWork.Videos.GetAllByTitle(sortingDirection, searchString);
                    break;
                case "uploader":
                    videos = UnitOfWork.Videos.GetAllByUploader(sortingDirection, searchString);
                    break;
                default:
                    videos = UnitOfWork.Videos.GetAllByDateUploaded(sortingDirection, searchString);
                    break;

            }
            ICollection<IndexViewModel> viewModel = videos.Select(video => new IndexViewModel(video)).ToList();

            return View(viewModel);
        }

        // GET: Videos/Details/5
        public ActionResult Details(int id = 0)
        {
            if (id <= 0)
                return HttpBadRequest();
            var video = UnitOfWork.Videos.GetIncludeRelated(id);
            if (video == null)
                return HttpNotFound();

            var videoIsPrivate = video.Visibility == PRIVATE;
            var videoIsBlocked = video.IsBlocked;

            if (AccountInSession == null)
            {
                if (videoIsPrivate || videoIsBlocked)
                    return Content(videoIsPrivate ? "This video is private." : "This video is blocked.");
            }
            else if (!AccountInSession.IsAdmin())
            {
                if (!AccountInSession.Is(video.Uploader) && (videoIsPrivate || videoIsBlocked))
                    return Content(videoIsPrivate ? "This video is private." : "This video is blocked.");
            }

            video.ViewsCount = ++video.ViewsCount;
            UnitOfWork.SaveChanges();

            return View(video);
        }

        [HttpGet]
        [AllowRoles(ADMIN, REGULAR_USER)]
        public ActionResult Upload()
        {
            return View(new Video());
        }

        [HttpPost]
        [AllowRoles(ADMIN, REGULAR_USER)]
        public ActionResult Upload(Video video)
        {
            if (!ModelState.IsValid)
                return View(video);
            if (AccountInSession.IsBlocked)
                return Content("You are blocked. You can not upload videos.");
            try
            {
                var uploader = UnitOfWork.Accounts.Get(AccountInSession.Id);
                video.Uploader = uploader;
                UnitOfWork.Videos.Add(video);
                UnitOfWork.SaveChanges();
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            return RedirectToAction("Details", "Accounts", new { username = video.Uploader.Username });
        }

        [AllowRoles(ADMIN, REGULAR_USER)]
        public ActionResult Edit(int id = 0)
        {
            if (id <= 0)
                return HttpBadRequest();
            var video = UnitOfWork.Videos.Get(id);
            if (video == null)
                return HttpNotFound();

            if (!AccountInSession.IsAdmin())
            {
                if (!AccountInSession.Is(video.Uploader))
                    return Content("Why would you even think you can edit someone else's video ?");
                if (AccountInSession.IsBlocked || video.IsBlocked)
                    return Content(AccountInSession.IsBlocked
                        ? "You are blocked. You can not edit your video."
                        : "This video is blocked. You can not edit it.");
            }

            return View(video);
        }

        [HttpPost]
        [AllowRoles(ADMIN, REGULAR_USER)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Video video)
        {
            if (id <= 0)
                return HttpBadRequest();
            if (!ModelState.IsValid)
                return View(video);
            var videoToUpdate = UnitOfWork.Videos.Get(video.Id);
            if (videoToUpdate == null)
                return HttpNotFound();


            if (!AccountInSession.IsAdmin())
            {
                if (!AccountInSession.Is(videoToUpdate.Uploader))
                    return Content("Why would you even think you can edit someone else's video ?");
                if (AccountInSession.IsBlocked || videoToUpdate.IsBlocked)
                    if (AccountInSession.IsBlocked || video.IsBlocked)
                        return Content(AccountInSession.IsBlocked
                            ? "You are blocked. You can not edit your video."
                            : "This video is blocked. You can not edit it.");
            }

            string[] valuesToUpdate =
            {
                "Title", "Description", "Visibility",
                "IsBlocked", "IsCommentingAllowed", "IsRatingVisible"
            };
            var modelUpdated = TryUpdateModel(videoToUpdate, "", valuesToUpdate);
            if (!modelUpdated)
                return View(video);
            try
            {
                UnitOfWork.SaveChanges();
            }
            catch (DataException)
            {
                return View("Error");
            }

            return RedirectToAction("Details", new { id = videoToUpdate.Id });

        }

        // POST: Videos/Delete/5
        [HttpPost]
        [AllowRoles(ADMIN, REGULAR_USER)]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id = 0)
        {
            if (id <= 0)
                return HttpBadRequest();
            var videoToDelete = UnitOfWork.Videos.GetIncludeRelated(id);
            if (videoToDelete == null)
                return HttpNotFound();
            try
            {
                UnitOfWork.Videos.Delete(videoToDelete);
                UnitOfWork.SaveChanges();
            }
            catch (DataException)
            {
                return RedirectToAction("Details", new { id });
            }

            return RedirectToAction("Index");
        }


    }
}
