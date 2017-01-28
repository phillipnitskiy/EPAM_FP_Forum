using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class PostViewModel
    {

        public int Id { get; set; }
        
        public UserViewModel User { get; set; }

        public DateTime CreationDate { get; set; }

        public string Text { get; set; }

        public bool Reported { get; set; }

    }
}