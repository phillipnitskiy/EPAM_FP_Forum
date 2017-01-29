using BLL.Interface.Services;
using MvcPL.Attributes;
using MvcPL.Infrastructure.Mappers;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcPL.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {

        #region Fields
        
        private readonly IUserService _userService;

        #endregion

        #region Constructor

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Public methods

        public ActionResult ManageUsers()
        {
            var users = _userService.GetAllUserEntities().Select(u => u.ToPlUser()).ToList();

            for (int i = 0; i < users.Count; i++)
            {
                users[i].Roles = Roles.GetRolesForUser(users[i].Login);
            }

            return View(users);
        }

        [HttpPost]
        [MultiButton(MatchFormKey = "manageUserAction", MatchFormValue = "AddRole")]
        public ActionResult AddRole(int id, string role)
        {
            var user = _userService.GetUserEntity(id);
            if (Roles.RoleExists(role) && !Roles.IsUserInRole(user.Login, role))
            {
                Roles.AddUserToRole(user.Login, role);
            }

            return RedirectToAction("ManageUsers");
        }

        [HttpPost]
        [MultiButton(MatchFormKey = "manageUserAction", MatchFormValue = "RemoveRole")]
        public ActionResult RemoveRole(int id, string role)
        {
            var user = _userService.GetUserEntity(id);
            if (Roles.RoleExists(role) && Roles.IsUserInRole(user.Login, role))
            {
                Roles.RemoveUserFromRole(user.Login, role);
            }

            return RedirectToAction("ManageUsers");
        }

        #endregion
    }
}