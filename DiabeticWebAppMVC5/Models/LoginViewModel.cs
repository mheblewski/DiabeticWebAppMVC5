using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiabeticWebAppMVC5.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter Your username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter Your password")]
        public string Password { get; set; }
    }
}