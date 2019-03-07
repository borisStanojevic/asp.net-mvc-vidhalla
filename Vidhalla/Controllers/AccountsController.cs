using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Vidhalla.Core.Domain;
using Vidhalla.Extensions;
using Vidhalla.Filters;
using Vidhalla.Persistence;
using Vidhalla.ViewModels.Accounts;
using static Vidhalla.Core.Domain.Role;

namespace Vidhalla.Controllers
{
    public class AccountsController : MyController
    {

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(SignUpViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);
            var account = UnitOfWork.Accounts.Get(a => a.Username.Equals(viewModel.Username));
            if (account != null)
            {
                ModelState.AddModelError("", "Username already exists");
                return View(viewModel);
            }

            account = new Account()
            {
                Username = viewModel.Username,
                Password = viewModel.Password,
                Role = REGULAR_USER
            };
            UnitOfWork.Accounts.Add(account);
            UnitOfWork.SaveChanges();

            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel viewModel)
        {
            var account = UnitOfWork.Accounts.Get(a => a.Username.Equals(viewModel.Username));
            if (account == null)
            {
                ModelState.AddModelError("", "Invalid username");
                return View(viewModel);
            }
            if (!account.Password.Equals(viewModel.Password))
            {
                ModelState.AddModelError("", "Invalid password");
                return View(viewModel);
            }

            var accountSessionModel = new AccountSessionModel
            {
                Id = account.Id,
                Username = account.Username,
                Role = account.Role,
                IsBlocked = account.IsBlocked
            };

            AccountInSession = accountSessionModel;

            return RedirectToAction("Index", "Videos");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Videos");
        }

        [Route("/accounts/details/{username:regex(^\\w{6,31}$)}")]
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

        [AllowRoles(ADMIN, REGULAR_USER)]
        public ActionResult Edit(int id)
        {
            if (id <= 0)
                return HttpBadRequest();
            var accountToEdit = UnitOfWork.Accounts.Get(id);
            if (accountToEdit == null)
                return HttpNotFound();

            if (!AccountInSession.IsAdmin())
            {
                if (!AccountInSession.Is(accountToEdit))
                    return Content("Why would you even think you can edit someone else's account ?");
                if (AccountInSession.IsBlocked)
                    return Content("You are blocked. You can not edit your account.");
            }

            return View(accountToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowRoles(ADMIN, REGULAR_USER)]
        public ActionResult Edit(int id, Account account)
        {
            if (id <= 0)
                return HttpBadRequest();
            if (!ModelState.IsValid)
                return View(account);
            var accountToEdit = UnitOfWork.Accounts.Get(account.Id);
            if (accountToEdit == null)
                return HttpNotFound();

            //Ako osoba koja pokusava da edituje nije admin onda...
            if (!AccountInSession.IsAdmin())
            {
                //Ako neko pokusava da edituje tudji nalog ne dozvoli
                if (!AccountInSession.Is(accountToEdit))
                    return Content("Why would you even think you can edit someone else's account ?");
                //U suprotnome proslo je prvu provjeru sto znaci da osoba koja edituje
                //je upravo osoba ciji se nalog edituje
                //Medjutim, ako je blokiran ne moze da edituje podatke
                if (AccountInSession.IsBlocked)
                    return Content("You are blocked. You can not edit your account.");
            }

            //Ako osoba pokusava da blokira sebe ili da si promijeni ulogu ne dozvoli bez obzira da li je admin
            if (AccountInSession.Is(accountToEdit) && (account.IsBlocked != accountToEdit.IsBlocked || account.Role != accountToEdit.Role))
                return Content("You can not block yourself nor change your role.");

            string[] valuesToUpdate =
            {
                "Password", "FirstName", "LastName",
                "ChannelDescription", "Role", "IsBlocked"
            };
            var modelUpdated = TryUpdateModel(accountToEdit, "", valuesToUpdate);
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

            return RedirectToAction("Details", new { username = accountToEdit.Username });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowRoles(ADMIN, REGULAR_USER)]
        public ActionResult UploadProfilePicture(HttpPostedFileBase picture, int accountId = 0)
        {
            if (picture == null)
                return HttpBadRequest();
            if (accountId <= 0 || string.Equals(picture.FileName, "default.png", StringComparison.OrdinalIgnoreCase))
                return HttpBadRequest();
            var account = UnitOfWork.Accounts.Get(accountId);
            if (account == null)
                return HttpNotFound();

            try
            {
                var picturesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                var pictureName = string.Concat(account.Username, Path.GetExtension(picture.FileName));
                var pictureFullPath = Path.Combine(picturesFolder, pictureName);

                if (System.IO.File.Exists(pictureFullPath))
                    System.IO.File.Delete(pictureFullPath);

                picture.SaveAs(pictureFullPath);

                account.ProfilePicture = pictureName;
                UnitOfWork.SaveChanges();
            }
            catch (SystemException)
            {
                return View("Error");
            }
            catch (Exception)
            {
                return View("Error");
            }

            return RedirectToAction("Details", new { username = account.Username });
        }

        public ActionResult LoadProfilePicture(string profilePicture)
        {
            var picturesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            var pictureFullPath = Path.Combine(picturesFolder, profilePicture);

            return File(pictureFullPath, "image/*");
        }

        [HttpPost]
        [AllowRoles(ADMIN, REGULAR_USER)]
        public ActionResult Subscribe(int id = 0)
        {
            if (AccountInSession == null)
                return RedirectToAction("Login");
            if (id <= 0)
                return HttpBadRequest();
            var beingSubscribedTo = UnitOfWork.Accounts.GetIncludeSubscribers(id);
            if (beingSubscribedTo == null)
                return HttpNotFound();

            if (AccountInSession.Is(beingSubscribedTo))
                return Content("You can not subscribe to yourself.");
            if (!AccountInSession.IsAdmin() && beingSubscribedTo.IsBlocked)
                return Content("You can not subscribe to this user. He is blocked.");
            if (!AccountInSession.IsAdmin() && AccountInSession.IsBlocked)
                return Content("You are blocked. You can not subscribe to anyone.");


            var isAlreadySubscribed = beingSubscribedTo.Subscribers.Any(s => AccountInSession.Is(s));
            if (isAlreadySubscribed)
                return Content("You are already subscribed. Perhaps you wanted to unsubscribe ?");

            var subscriber = UnitOfWork.Accounts.Get(AccountInSession.Id);
            beingSubscribedTo.Subscribers.Add(subscriber);
            UnitOfWork.SaveChanges();

            return Json(new { nextAction = "Unsubscribe" });
        }

        [HttpPost]
        [AllowRoles(ADMIN, REGULAR_USER)]
        public ActionResult Unsubscribe(int id = 0)
        {
            if (AccountInSession == null)
                return RedirectToAction("Login");
            if (id <= 0)
                return HttpBadRequest();
            var beingUnsubscribedFrom = UnitOfWork.Accounts.GetIncludeSubscribers(id);
            if (beingUnsubscribedFrom == null)
                return HttpNotFound();

            if (AccountInSession.Is(beingUnsubscribedFrom))
                return Content("Why would you even try to unsubscribe from yourself in the first place ?");

            var isAlreadySubscribed = beingUnsubscribedFrom.Subscribers.Any(s => AccountInSession.Is(s));
            if (!isAlreadySubscribed)
                return Content("Why would you try to unsubscribe when you are not even subscribed yet ?");

            var subscriber = UnitOfWork.Accounts.Get(AccountInSession.Id);
            beingUnsubscribedFrom.Subscribers.Remove(subscriber);
            UnitOfWork.SaveChanges();


            return Json(new { nextAction = "Subscribe" });
        }
    }
}