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

        public ActionResult Unauthenticated()
        {
            return Content("Neulogovan korisnik vidi");
        }

        [AllowRoles(REGULAR_USER)]
        public ActionResult Authenticated()
        {
            return Content("Ulogovan vidi");
        }

        public ActionResult Login()
        {
            return Content("LOGIN STRANICA");
        }

        //public ActionResult Login()
        //{
        //    //Ako korisnik nije autentikovan vrati ga na login ako jeste vrati ga na indeks
        //}

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var account = UnitOfWork.Accounts.Get(a => a.Username.Equals(username));
            if (account == null)
            {
                ModelState.AddModelError("", "Invalid username");
                return View();
            }
            if (!account.Password.Equals(password))
            {
                ModelState.AddModelError("", "Invalid password");
                return View();
            }

            var accountSessionModel = new AccountSessionModel
            {
                Username = account.Username,
                Role = account.Role,
                IsBlocked = account.IsBlocked
            };

            Session.SetAccount(accountSessionModel);

            return RedirectToAction("Index", "Videos");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Videos");
        }


        [Route("accounts/details/{username:regex(^\\w{6,31}$)}")]
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
            var account = UnitOfWork.Accounts.Get(id);
            if (account == null)
                return HttpNotFound();

            return View(account);
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
            var accountToUpdate = UnitOfWork.Accounts.Get(account.Id);
            if (accountToUpdate == null)
                return HttpNotFound();

            string[] valuesToUpdate =
            {
                "Password", "FirstName", "LastName",
                "ChannelDescription", "Role", "IsBlocked"
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

            return RedirectToAction("Details", new { username = accountToUpdate.Username });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowRoles(ADMIN, REGULAR_USER)]
        public ActionResult UploadProfilePicture(int accountId, HttpPostedFileBase picture)
        {

            //var picture = uploadPictureViewModel.PictureFile;
            if (picture == null)
                return HttpBadRequest();
            //var id = uploadPictureViewModel.AccountId;
            if (accountId <= 0 || string.Equals(picture.FileName, "default.png", StringComparison.OrdinalIgnoreCase))
                return HttpBadRequest();
            var account = UnitOfWork.Accounts.Get(accountId);
            if (account == null)
                return HttpNotFound();

            try
            {
                var picturesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                Debug.WriteLine(picturesFolder);
                var pictureName = string.Concat(account.Username, Path.GetExtension(picture.FileName));

                Debug.WriteLine(pictureName);
                var pictureFullPath = Path.Combine(picturesFolder, pictureName);
                Debug.WriteLine(pictureFullPath);

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


    }
}