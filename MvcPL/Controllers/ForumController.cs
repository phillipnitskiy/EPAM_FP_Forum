using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models.Input;
using MvcPL.Models.View;
using MvcPL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcPL.Controllers
{
    [Authorize]
    public class ForumController : Controller
    {

        #region Fields

        private readonly IBoardService _boardService;
        private readonly ITopicService _topicService;
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        private readonly int DefaultPageSize = 7;

        #endregion

        #region Constructor

        public ForumController(IBoardService boardService, ITopicService topicService, IPostService postService, IUserService userService)
        {
            _boardService = boardService;
            _topicService = topicService;
            _postService = postService;
            _userService = userService;
        }

        #endregion

        #region Public methods

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Board(int id, int? page)
        {
            var board = _boardService.GetBoard(id)
                ?.ToPlBoard();

            if (board == null)
                return HttpNotFound();

            var forumBoardViewModel = new ForumBoardViewModel { Board = board };
            forumBoardViewModel.TopicInput.BoardId = id;
            forumBoardViewModel.BoardInput.ParentId = id;
            forumBoardViewModel.ParentBoards = _boardService.GetBoardParents(id).Select(b => b.ToPlBoard()).Reverse().ToList();

            if (board.SubBoards.Count + board.Topics.Count > DefaultPageSize)
            {
                var pageInfo = new PageInfoModel
                {
                    PageNumber = page ?? 1,
                    PageSize = DefaultPageSize,
                    TotalItems = board.SubBoards.Count + board.Topics.Count
                };

                var skipped = board.SubBoards.Count;
                board.SubBoards = board.SubBoards.Skip(((page ?? 1) - 1) * DefaultPageSize).Take(DefaultPageSize).ToList();
                skipped -= board.SubBoards.Count;

                var left = DefaultPageSize - board.SubBoards.Count();
                board.Topics = board.Topics.Skip(((page ?? 1) - 1) * DefaultPageSize - skipped).Take(left).ToList();

                forumBoardViewModel.PageInfo = pageInfo;
            }

            return View(forumBoardViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult AddBoard(BoardInputModel boardInput, int parentId)
        {
            if (ModelState.IsValid)
            {
                boardInput.ParentId = parentId;

                _boardService.CreateBoard(boardInput.ToBllBoard());

                if (Url.IsLocalUrl(HttpContext.Request.UrlReferrer.AbsolutePath))
                {
                    return Redirect(HttpContext.Request.UrlReferrer.AbsolutePath);
                }

                return RedirectToAction("Board", "Forum", new { id = parentId });
            }
            else
            {
                ModelState.AddModelError("", "Incorrect input.");

                var board = _boardService.GetBoard(parentId)
                    ?.ToPlBoard();

                if (board == null)
                    return View("Error");

                var forumBoardViewModel = new ForumBoardViewModel { Board = board };
                forumBoardViewModel.TopicInput.BoardId = parentId;
                forumBoardViewModel.BoardInput = boardInput;

                return View("Board", forumBoardViewModel);
            }

        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Topic(int id)
        {
            var topic = _topicService.GetTopic(id)
                ?.ToPlTopic();

            if (topic == null)
                return HttpNotFound();

            foreach (var post in topic.Posts)
            {
                post.User = _userService.GetUserEntity(post.User.Id).ToPlUser();
            }

            var forumTopicViewModel = new ForumTopicViewModel { Topic = topic };
            forumTopicViewModel.PostInput.TopicId = id;
            forumTopicViewModel.ParentBoards = _topicService.GetTopicParents(id).Select(b => b.ToPlBoard()).Reverse().ToList();

            return View(forumTopicViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTopic(TopicInputModel topicInput, int boardId)
        {

            if (ModelState.IsValid)
            {
                var topicEntity = topicInput.ToBllTopic();
                topicEntity.BoardId = boardId;
                topicEntity.CreationDate = DateTime.Now;
                topicEntity.UserId = _userService.GetUserEntityByLogin(User.Identity.Name).Id;

                _topicService.CreateTopic(topicEntity);

                var postEntity = topicInput.Comment.ToBllPost();
                postEntity.TopicId = _topicService.GetTopicBySubject(topicInput.Subject).Id;
                postEntity.CreationDate = DateTime.Now;
                postEntity.UserId = _userService.GetUserEntityByLogin(User.Identity.Name).Id;

                _postService.CreatePost(postEntity);

                if (Url.IsLocalUrl(HttpContext.Request.UrlReferrer.AbsolutePath))
                {
                    return Redirect(HttpContext.Request.UrlReferrer.AbsolutePath);
                }

                return RedirectToAction("Board", "Forum", new { id = boardId });
            }
            else
            {
                ModelState.AddModelError("", "Incorrect input.");

                var board = _boardService.GetBoard(boardId)
                    ?.ToPlBoard();

                if (board == null)
                    return View("Error");

                var forumBoardViewModel = new ForumBoardViewModel { Board = board };
                forumBoardViewModel.TopicInput = topicInput;
                forumBoardViewModel.BoardInput.ParentId = boardId;

                return View("Board", forumBoardViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPost(PostInputModel postInput, int topicId)
        {
            if (ModelState.IsValid)
            {
                var postEntity = postInput.ToBllPost();
                postEntity.TopicId = topicId;
                postEntity.CreationDate = DateTime.Now;
                postEntity.UserId = _userService.GetUserEntityByLogin(User.Identity.Name).Id;

                _postService.CreatePost(postEntity);

                if (Request.IsAjaxRequest())
                {
                    var post = _postService.GetTopicPosts(topicId).LastOrDefault().ToPlPost();
                    post.User = _userService.GetUserEntity(post.User.Id).ToPlUser();
                    return PartialView("_Post", post);
                }
                else
                {
                    return RedirectToAction("Board", "Topic", new { id = topicId });
                }
            }
            else
            {
                ModelState.AddModelError("", "Incorrect input.");

                var topic = _topicService.GetTopic(topicId)
                ?.ToPlTopic();

                if (topic == null)
                    return View("Error");

                var forumTopicViewModel = new ForumTopicViewModel { Topic = topic };
                forumTopicViewModel.PostInput = postInput;

                return View("Topic", forumTopicViewModel);
            }
        }

        [AllowAnonymous]
        public ActionResult FindTopic(string term)
        {
            var topics = _topicService.FindTopics(term).Select(t => t.ToPlTopic());

            if (Request.IsAjaxRequest())
            {

                var projection = topics.Select(t => new
                {
                    id = t.Id,
                    label = t.Subject,
                    value = t.Subject
                });

                return Json(projection.ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (topics.Count() == 1)
                {
                    return RedirectToAction("Topic", "Forum", new { id = topics.First().Id });
                }

                return View(topics);
                
            }

        }

        [HttpPost]
        public ActionResult ReportPost(int postId)
        {
            _postService.ReportPost(postId);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Reported");
            }
            else
            {
                var post = _postService.GetPostEntity(postId);
                return RedirectToAction("Topic", new { topicId = post.TopicId });
            }
        }

        #endregion
    }
}