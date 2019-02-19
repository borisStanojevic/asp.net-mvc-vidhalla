using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidhalla.Core.Domain;
using Vidhalla.Persistence;

namespace Vidhalla.Controllers
{
    public class AccountsController : MyController
    {
        // GET: Details/{id}
        public ActionResult Details(int? id, string username)
        {
            return View();
        }

        // GET: Edit/{id}
        public ActionResult Edit(int id)
        {
            Account account = UnitOfWork.Accounts.Get(id);
            if (account == null)
                return HttpNotFound();

            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Account account)
        {
            if (!ModelState.IsValid)
                return View(account);

            Account accountToUpdate = UnitOfWork.Accounts.Get(account.Id);
            string[] valuesToUpdate =
            {
                "Username", "Password",
                "FirstName", "LastName",
                "ChannelDescription", "Role",
                "IsBlocked"
            };
            TryUpdateModel(accountToUpdate, "", valuesToUpdate);
            UnitOfWork.SaveChanges();
            return RedirectToAction("Details", new {id = accountToUpdate.Id});
        }


    }
}