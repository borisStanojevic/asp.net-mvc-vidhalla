using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidhalla.Core.Domain;
using Vidhalla.Filters;
using Vidhalla.ViewModels.Comments;
using static Vidhalla.Core.Domain.Role;

namespace Vidhalla.Controllers
{
    public class CommentsController : MyController
    {
        [HttpPost]
        public ActionResult Index(int videoId, string sortOrder = "datePosted-desc")
        {
            SortingDirection sortingDirection;

            try
            {
                sortingDirection = sortOrder.Split('-')[1].Equals("desc") ? SortingDirection.DESC : SortingDirection.ASC;
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }

            var comments = UnitOfWork.Comments.GetAllByDatePosted(c => c.Video.Id == videoId, sortingDirection);
            ICollection<DetailsViewModel> commentsViewModels = comments.Select(comment => new DetailsViewModel
            {
                Id = comment.Id,
                CommenterProfilePicture = comment.Commenter.ProfilePicture,
                CommenterUsername = comment.Commenter.Username,
                DatePosted = comment.DatePosted.ToShortDateString(),
                Content = comment.Content
            })
                .ToList();

            return Json(commentsViewModels);
        }


        [HttpPost]
        [AllowRoles(ADMIN, REGULAR_USER)]
        public ActionResult Create(string content, int videoId)
        {
            if (AccountInSession == null)
                return RedirectToAction("Login", "Accounts");

            var video = UnitOfWork.Videos.Get(videoId);
            var commenter = UnitOfWork.Accounts.Get(AccountInSession.Id);
            if (video == null)
                return HttpNotFound();

            if (!AccountInSession.IsAdmin())
            {
                if (AccountInSession.IsBlocked)
                    return Json(new { errorMessage = "You can not post comments while blocked." });
                if (video.IsBlocked)
                    return Json(new { errorMessage = "You can not post comments on a blocked video." });
                if (!video.IsCommentingAllowed)
                    return Json(new { errorMessage = "Commenting is not allowed for this video." });
            }

            var newComment = new Comment
            {
                Video = video,
                Commenter = commenter,
                DatePosted = DateTime.Now,
                Content = content,
                IsDeleted = false
            };

            UnitOfWork.Comments.Add(newComment);
            UnitOfWork.SaveChanges();

            var viewModel = new DetailsViewModel
            {
                Id = newComment.Id,
                CommenterProfilePicture = commenter.ProfilePicture,
                CommenterUsername = commenter.Username,
                DatePosted = DateTime.Now.ToShortDateString(),
                Content = content
            };

            return Json(viewModel);
        }

        [HttpPost]
        [AllowRoles(ADMIN, REGULAR_USER)]
        public ActionResult Delete(int id = 0)
        {
            if (AccountInSession == null)
                return RedirectToAction("Login", "Accounts");
            if (id <= 0)
                return HttpBadRequest();
            var commentToDelete = UnitOfWork.Comments.Get(id);
            if (commentToDelete == null)
                return HttpNotFound();

            if (!AccountInSession.IsAdmin() && !AccountInSession.Is(commentToDelete.Commenter))
                return Json(new { errorMessage = "Why would you even think you can delete someone else's comment ?" });

            UnitOfWork.Comments.Delete(commentToDelete);
            UnitOfWork.SaveChanges();

            return Json(new { message = "Comment deleted" });
        }

    }
}