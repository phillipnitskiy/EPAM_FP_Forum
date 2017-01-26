using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models.Input;
using MvcPL.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        #region Fields

        private readonly IBoardService _boardService;

        #endregion

        #region Constructor

        public HomeController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        #endregion

        #region Public methods

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            var mainBoards = _boardService.GetSubBoards(null);

            var homeViewModel = new HomeViewModel();

            foreach (var board in mainBoards)
            {
                var b = board.ToPlBoard();

                var subBoards = _boardService.GetSubBoards(b.Id).OrderBy(boa => boa.Id).Take(3);
                foreach (var subBoard in subBoards)
                {
                    b.SubBoards.Add(subBoard.ToPlBoard());
                }

                homeViewModel.boards.Add(b);
            }

            return View(homeViewModel);
        }

        #endregion
    }
}
