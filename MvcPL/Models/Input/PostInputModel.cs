using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models.Input
{
    public class PostInputModel
    {
        [ScaffoldColumn(false)]
        public int TopicId { get; set; }

        [Display(Name = "Text")]
        [Required(ErrorMessage = "Please enter your post text.")]
        public string Text { get; set; }

    }
}