using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models.Input
{
    public class UserRegistrationInputModel
    {

        [Display(Name = "Login")]
        [Required(ErrorMessage = "Please enter your login.")]
        [System.Web.Mvc.Remote("VerifyUserExists","Account", ErrorMessage = "This login is already exist.")]
        [RegularExpression(@"[A-Za-z0-9._-]+", ErrorMessage = "The login can contain only [A-Za-z0-9._-] symbols.")]
        [StringLength(30, ErrorMessage = "The login must contain at least {2} characters.", MinimumLength = 4)]
        public string Login { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter your email.")]
        [System.Web.Mvc.Remote("VerifyEmailExists","Account", ErrorMessage = "This email is already in use.")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Please enter a valid email.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter your password.")]
        [StringLength(20, ErrorMessage = "The password must contain at least {2} characters.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm")]
        [Required(ErrorMessage = "Please confirm your password.")]
        [Compare("Password", ErrorMessage = "Passwords must match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}