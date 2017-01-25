using MvcPL.Models.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models.View
{
    public class ForumBoardViewModel
    {
        public ForumBoardViewModel()
        {
            ParentBoards = new List<BoardViewModel>();
            BoardInput = new BoardInputModel();
            TopicInput = new TopicInputModel();
        }

        public BoardViewModel Board { get; set; }

        public List<BoardViewModel> ParentBoards { get; set; }

        public BoardInputModel BoardInput { get; set; }
        public TopicInputModel TopicInput { get; set; }
    }
}