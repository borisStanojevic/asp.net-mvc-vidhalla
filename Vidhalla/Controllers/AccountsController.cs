using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vidhalla.Core.Domain;
using Vidhalla.Persistence;
using Vidhalla.ViewModels.Accounts;

namespace Vidhalla.Controllers
{
    public class AccountsController : MyController
    {
        [Route("accounts/details/{username:regex(^\\w{6,31}$}")]
        public ActionResult Details(string username)
        {
            if (username == null)
                return HttpBadRequest();
            var account = UnitOfWork.Accounts.GetIncludeRelated(username);
            if (account == null)
                return HttpNotFound();
            var subscribeds = UnitOfWork.Accounts.GetSubscribeds(account.Id);

            var viewModel = new DetailsViewModel(account, subscribeds);

            return View(viewModel);
        }


        public ActionResult Edit(int id)
        {
            if (id <= 0)
                return HttpBadRequest();
            var account = UnitOfWork.Accounts.Get(id);
            if (account == null)
                return HttpNotFound();

            return View(account);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Account account)
        {
            if (id <= 0)
                return HttpBadRequest();
            if (!ModelState.IsValid)
                return View(account);
            var accountToUpdate = UnitOfWork.Accounts.Get(account.Id);
            if (accountToUpdate == null)
                return HttpNotFound();

            string[] valuesToUpdate =
            {
                "Password", "IsBlocked", "FirstName",
                "LastName", "ChannelDescription", "Role",
            };
            var modelUpdated = TryUpdateModel(accountToUpdate, "", valuesToUpdate);
            if (!modelUpdated)
                return View(account);
            try
            {
                UnitOfWork.SaveChanges();
            }
            catch (DataException)
            {
                return View("Error");
            }

            return View("Details", accountToUpdate);
        }


    }
}