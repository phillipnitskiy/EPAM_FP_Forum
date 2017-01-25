using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models.View
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            boards = new List<BoardViewModel>();
        }

        public List<BoardViewModel> boards { get; set; }

    }
}