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
    [Route("/video-votes")]
    public class VideoVotesController : MyController
    {

        [HttpPost]
        [AllowRoles(ADMIN, REGULAR_USER)]
        public ActionResult Create(int videoId, Vote type)
        {
            var videoVote = UnitOfWork.VideoVotes.Get(vv => (vv.Video_Id == videoId && vv.Owner_Id == AccountInSession.Id));
            //Ako ne uspije vratit vote znaci da se prvi put lajkuje ili dislajkuje pa napravi i dodaj
            //var actionTaken = null;
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
                UnitOfWork.SaveChanges();
            }
            //Ako je vraceni vote Like
            else if ((videoVote.Type == LIKE && type == LIKE) || (videoVote.Type == DISLIKE && type == DISLIKE))
                UnitOfWork.VideoVotes.Delete(videoVote);
            else if (videoVote.Type == LIKE && type == DISLIKE)
                videoVote.Type = DISLIKE;
            else if (videoVote.Type == DISLIKE && type == LIKE)
                videoVote.Type = LIKE;

        }
    }
}