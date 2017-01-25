using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class BoardViewModel
    {

        public BoardViewModel()
        {
            SubBoards = new List<BoardViewModel>();
            Topics = new List<TopicViewModel>();

            LastPostUserName = "User Name";
            LastPostDate = DateTime.Now;
            TopicCount = 12334;
            PostCount = 54332;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<BoardViewModel> SubBoards { get; set; }
        public List<TopicViewModel> Topics { get; set; }

        public int TopicCount { get; set; }
        public int PostCount { get; set; }

        public DateTime LastPostDate { get; set; }
        public string LastPostUserName { get; set; }

    }
}