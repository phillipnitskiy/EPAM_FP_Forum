using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class TopicViewModel
    {

        public TopicViewModel()
        {
            Posts = new List<PostViewModel>();
        }

        public int Id { get; set; }

        public int BoardId { get; set; }

        public string Subject { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastPostDate { get; set; }
        public string LastPostUserName { get; set; }

        public int PostCount { get; set; }

        public List<PostViewModel> Posts { get; set; }

    }
}