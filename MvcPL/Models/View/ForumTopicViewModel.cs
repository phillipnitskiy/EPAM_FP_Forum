using MvcPL.Models.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models.View
{
    public class ForumTopicViewModel
    {
        public ForumTopicViewModel()
        {
            ParentBoards = new List<BoardViewModel>();
            PostInput = new PostInputModel();
        }

        public TopicViewModel Topic { get; set; }

        public List<BoardViewModel> ParentBoards { get; set; }

        public PostInputModel PostInput { get; set; }

    }
}