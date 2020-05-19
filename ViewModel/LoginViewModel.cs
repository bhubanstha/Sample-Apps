﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Not a valid email id")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
