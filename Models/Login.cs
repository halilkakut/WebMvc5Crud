using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationProductModule.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Please Enter Username !")]
        [Display(Name = "Enter Username :")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Password !")]
        [Display(Name = "Enter Password :")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}