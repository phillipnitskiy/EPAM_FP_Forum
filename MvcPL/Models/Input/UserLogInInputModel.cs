using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models.Input
{
    public class UserLogInInputModel
    {
        [Display(Name = "Login")]
        [Required(ErrorMessage = "Please enter your login.")]
        [RegularExpression(@"[A-Za-z0-9._-]+", ErrorMessage = "The login can contain only [A-Za-z0-9._-] symbols.")]
        [StringLength(30, ErrorMessage = "The login must contain at least {2} characters.", MinimumLength = 4)]
        public string Login { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember?")]
        public bool RememberMe { get; set; }

    }
}