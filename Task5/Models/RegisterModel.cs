using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task5.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Username is required!")]
        [Display(Name ="Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [Display(Name = "Password")]
        [MinLength(5, ErrorMessage = "Password must have at least 5 char!")]
        [Compare("Password2", ErrorMessage ="Password 1 does not match with Password 2!")]
        public string Password { get; set; }
        public string Password2 { get; set; }

        public Pets Preferences { get; set; }
    }
}