using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class UserViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Login")]
        public string Login { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Registration date")]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "Roles")]
        public IEnumerable<string> Roles { get; set; }

    }
}