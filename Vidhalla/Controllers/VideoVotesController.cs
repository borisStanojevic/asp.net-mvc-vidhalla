using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidhalla.Core.Domain;
using Vidhalla.Filters;
using Vidhalla.Persistence;
using static Vidhalla.Core.Domain.Role;
using static Vidhalla.Core.Domain.Vote;

namespace Vidhalla.Controllers
{
    public class VideoVotesController : MyController
    {

        [HttpPost]
        [AllowRoles(ADMIN, REGULAR_USER)]
        public ActionResult Create(int videoId, Vote type)
        {
            if (AccountInSession == null)
                return RedirectToAction("Login", "Accounts");
            var video = UnitOfWork.Videos.Get(videoId);
            if (video == null)
                return HttpNotFound();
            if (!AccountInSession.IsAdmin())
            {
                if (AccountInSession.IsBlocked)
                    return Json(new { errorMessage = "You can not like or dislike while blocked." });
                if (video.IsBlocked)
                    return Json(new { errorMessage = "You can not like or dislike a blocked video." });
            }
            var videoVote = UnitOfWork.VideoVotes.Get(vv => (vv.Video_Id == videoId && vv.Owner_Id == AccountInSession.Id));
            //Ako ne uspije vratit vote znaci da se prvi put lajkuje ili dislajkuje pa napravi i dodaj
            var currentVoteStatus = "NONE";
            if (videoVote == null)
            {
                videoVote = new VideoVote
                {
                    Owner_Id = AccountInSession.Id,
                    Video_Id = videoId,
                    DateCreated = DateTime.Now,
                    Type = type
                };
                UnitOfWork.VideoVotes.Add(videoVote);
                currentVoteStatus = videoVote.Type == LIKE ? "LIKED" : "DISLIKED";
            }
            //Ako je korisnik kliknuo like a pri tome je vec lajkovao, ukloni lajk odnosno postani neutralan
            //isto vazi i za dislike
            else if ((videoVote.Type == LIKE && type == LIKE) || (videoVote.Type == DISLIKE && type == DISLIKE))
                UnitOfWork.VideoVotes.Delete(videoVote);
            //Ako pokusava da dislajkuje a prethodno je lajkovao, promijeni vote u dislike
            else if (videoVote.Type == LIKE && type == DISLIKE)
            {
                videoVote.Type = DISLIKE;
                currentVoteStatus = "DISLIKED";
            }
            //Ako pokusava da lajkuje a prethodno je dislajkovao, promijeni vote u like
            else if (videoVote.Type == DISLIKE && type == LIKE)
            {
                videoVote.Type = LIKE;
                currentVoteStatus = "LIKED";
            }

            UnitOfWork.SaveChanges();

            return Json(currentVoteStatus);

        }
    }
}