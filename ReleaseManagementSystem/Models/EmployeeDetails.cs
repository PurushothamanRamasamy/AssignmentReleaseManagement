using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReleaseManagementSystem.Models
{
    
   
    public class EmployeeDetails
    {
        [Required (ErrorMessage ="Employee Id should not empty")]
        [Display(Name ="Employee Id")]

        [Key]
        public string Employee_Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5)]
        [RegularExpression(@"[a-zA-Z]*", ErrorMessage = " Space and numbers not allowed")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password can not be empty")]
       // [RegularExpression("^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$", ErrorMessage = "Password should contain at least onelower case letter, oneupper case letter,special character, one number, and 8 characters length")]
        public string Password { get; set; }

        
       
        [Required(ErrorMessage = "Role can not be empty")]
        public string Role { get; set; }

    }
}