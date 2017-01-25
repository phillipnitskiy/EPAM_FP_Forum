using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interface.Services;
using MvcPL.Models.Input;
using MvcPL.Providers;
using System.Web;
using BLL.Interface.Entities;
using System.IO;
using System;
using System.Text;
using MvcPL.Infrastructure.Mappers;

namespace MvcPL.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly IProfileService _profileService;

        #endregion

        #region Constructor

        public AccountController(IUserService userService, IProfileService profileService)
        {
            _userService = userService;
            _profileService = profileService;
        }

        #endregion

        #region Public methods

        [HttpGet]
        public ActionResult Index()
        {
            var userProfileinput = new UserProfileInputModel();

            userProfileinput.Id = _userService.GetUserEntityByLogin(User.Identity.Name).Id;

            var profileEntity = _profileService.GetProfileEntity(userProfileinput.Id);
            if (profileEntity != null)
            {
                userProfileinput.ImageData = profileEntity.ImageData;
                userProfileinput.ImageMimeType = profileEntity.ImageMimeType;
            }

            return View(userProfileinput);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult LogIn()
        {
            //if (User.Identity.IsAuthenticated)
            //    return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(UserLogInInputModel userLogInInputModel)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(userLogInInputModel.Login, userLogInInputModel.Password))
                {
                    FormsAuthentication.SetAuthCookie(userLogInInputModel.Login, userLogInInputModel.RememberMe);

                    if (Url.IsLocalUrl(HttpContext.Request.UrlReferrer.AbsolutePath))
                    {
                        return Redirect(HttpContext.Request.UrlReferrer.AbsolutePath);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
            }

            ModelState.AddModelError("", "Incorrect login or password.");

            return View(userLogInInputModel);
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            if (Url.IsLocalUrl(HttpContext.Request.UrlReferrer.AbsolutePath))
            {
                    return Redirect(HttpContext.Request.UrlReferrer.AbsolutePath);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            //if (User.Identity.IsAuthenticated)
            //    return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserRegistrationInputModel userRegistrationInputModel)
        {

            if (ModelState.IsValid)
            {
                var userWithSameLogin = Membership.GetUser(userRegistrationInputModel.Login);
                if (userWithSameLogin != null)
                {
                    ModelState.AddModelError("", "This login is already exist.");
                    return View(userRegistrationInputModel);
                }

                var userWithSameEmail = Membership.GetUserNameByEmail(userRegistrationInputModel.Email);
                if (userWithSameEmail != null)
                {
                    ModelState.AddModelError("", "This email is already in use.");
                    return View(userRegistrationInputModel);
                }

                var membershipUser = ((CustomMembershipProvider)Membership.Provider).CreateUser(userRegistrationInputModel);

                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(userRegistrationInputModel.Login, false);
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Incorrect input.");

            return View(userRegistrationInputModel);
        }

        [AllowAnonymous]
        public JsonResult VerifyUserExists(string login)
        {
            MembershipUser userWithSameLogin = Membership.GetUser(login);

           return Json(userWithSameLogin == null, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult VerifyEmailExists(string email)
        {
            var userWithSameEmail = Membership.GetUserNameByEmail(email);

            return Json(userWithSameEmail == null, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(UserProfileInputModel userProfileInput, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    userProfileInput.ImageMimeType = image.ContentType;
                    userProfileInput.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(userProfileInput.ImageData, 0, image.ContentLength);
                }

                userProfileInput.Id = _userService.GetUserEntityByLogin(User.Identity.Name).Id;

                var profileEntity = userProfileInput.ToBllProfile();
                if (_profileService.GetProfileEntity(profileEntity.Id) == null)
                {
                    _profileService.CreateProfile(profileEntity);

                }
                else
                {
                    _profileService.UpdateProfile(profileEntity);
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(userProfileInput);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public FileContentResult GetImage(int id)
        {
            var profile = _profileService.GetProfileEntity(id);

            if (profile != null)
            {
                return File(profile.ImageData, profile.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}
