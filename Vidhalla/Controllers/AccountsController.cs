using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vidhalla.Core.Domain;
using Vidhalla.Persistence;

namespace Vidhalla.Controllers
{
    public class AccountsController : MyController
    {
        //    GET: Details/{id
        //}
        //public ActionResult Details(int id, string username)
        //{
        //    if (id <= 0 && username == null)
        //        return HttpBadRequest();
        //    Account account;

        //}

        // GET: Edit/{id}
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
                //Uradi nesto sa exceptionom ?        
            }

            return View("Details", accountToUpdate);
        }
    }
}