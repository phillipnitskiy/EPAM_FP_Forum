using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models.Input
{
    public class BoardInputModel
    {
        [ScaffoldColumn(false)]
        public int ParentId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter your board name.")]
        [StringLength(300, ErrorMessage = "The board name must contain at least {2} characters.", MinimumLength = 5)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(300)]
        public string Description { get; set; }
    }
}