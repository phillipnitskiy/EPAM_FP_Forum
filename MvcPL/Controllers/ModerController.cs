using BLL.Interface.Services;
using MvcPL.Attributes;
using MvcPL.Infrastructure.Mappers;
using System.Linq;
using System.Web.Mvc;

namespace MvcPL.Controllers
{
    [Authorize(Roles = "Moderator")]
    public class ModerController : Controller
    {

        #region Fields
        
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        #endregion

        #region Constructor

        public ModerController(IUserService userService, IPostService postService)
        {
            _userService = userService;
            _postService = postService;
        }

        #endregion

        #region Public methods

        [HttpGet]
        public ActionResult ReportedPosts()
        {
            var reportedPosts = _postService.GetReportedPosts()
                .Select(p => p.ToPlPost()).ToList();

            for (int i = 0; i < reportedPosts.Count; i++)
            {
                reportedPosts[i].User = _userService.GetUserEntity(reportedPosts[i].User.Id).ToPlUser();
            }

            return View(reportedPosts);
        }

        [HttpPost]
        [MultiButton(MatchFormKey = "managePostAction", MatchFormValue = "UnreportPost")]
        public ActionResult UnreportPost(int id)
        {
            _postService.UnreportPost(id);
            if (Request.IsAjaxRequest())
            {
                return null;
            }
            else
            {
                return RedirectToAction("ReportedPosts");
            }
        }

        [HttpPost]
        [MultiButton(MatchFormKey = "managePostAction", MatchFormValue = "DeletePost")]
        public ActionResult DeletePost(int id)
        {
            _postService.DeletePost(id);
            if (Request.IsAjaxRequest())
            {
                return null;
            }
            else
            {
                return RedirectToAction("ReportedPosts");
            }

        }

        #endregion
    }
}