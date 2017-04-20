using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models.Input
{
    public class TopicInputModel
    {
        [ScaffoldColumn(false)]
        public int BoardId { get; set; }

        [Display(Name = "Subject")]
        [Required(ErrorMessage = "Please enter your topic subject.")]
        [StringLength(300, ErrorMessage = "The topic subject must contain at least {2} characters.", MinimumLength = 5)]
        public string Subject { get; set; }

        [Display(Name = "Comment")]
        [Required(ErrorMessage = "Please enter your comment text.")]
        public PostInputModel Comment { get; set; }

    }
}