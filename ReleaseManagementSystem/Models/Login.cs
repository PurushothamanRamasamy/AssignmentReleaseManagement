using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReleaseManagementSystem.Models
{
    public class Login
    {
        [Required]
        [RegularExpression(@"[a-zA-Z]*", ErrorMessage = " Space and numbers not allowed")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password can not be empty")]
        
        public string Password { get; set; }
    }
}