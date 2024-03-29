﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Models
{
    public class Register
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string  Name { get; set; }

        [Required]
        [EmailAddress]
        public string  Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage ="The {0} must be atleast {2} characters long", MinimumLength =6)]
        public string  Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        [Compare("Password", ErrorMessage ="Passwords Mismatch")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name ="Role Name")]
        public string RoleName { get; set; }
    }
}
