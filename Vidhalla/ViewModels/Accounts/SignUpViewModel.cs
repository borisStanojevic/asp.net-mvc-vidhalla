using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidhalla.ViewModels.Accounts
{
    public class SignUpViewModel
    {
        [Required]
        [RegularExpression(@"^\w{6,31}$", ErrorMessage = "Username can contain letters, digits and underscore and must be between 6 and 32 characters long")]
        public string Username { get; set; }

        [Required]
        [StringLength(127)]
        public string Password { get; set; }

        [Display(Name = "First Name")]
        [StringLength(31)]
        public string FirstName { get; set; } = "";

        [Display(Name = "Last Name")]
        [StringLength(63)]
        public string LastName { get; set; } = "";
    }
}